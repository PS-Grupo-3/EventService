using Application.Features.Event.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Requests;
using EventEntity = Domain.Entities.Event;
using Moq;

namespace Tests.Event.Commands
{
    public class UpdateEventHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateExistingEvent_WhenDataIsValid()
        {
            var eventCommand = new Mock<IEventCommand>();
            var eventQuery = new Mock<IEventQuery>();

            var existing = new EventEntity
            {
                EventId = Guid.NewGuid(),
                Name = "Viejo nombre",
                UserToken = "null"
            };

            eventQuery
                .Setup(x => x.GetByIdAsync(existing.EventId, default))
                .ReturnsAsync(existing);

            var handler = new UpdateEventHandler(eventCommand.Object, eventQuery.Object);

            var req = new UpdateEventRequest
            {
                EventId = existing.EventId,
                Name = "Nuevo nombre"
            };

            var result = await handler.Handle(new UpdateEventCommand(req), default);

            Assert.True(result.Success);
            Assert.Equal("Nuevo nombre", existing.Name);
            eventCommand.Verify(x => x.UpdateAsync(existing, default), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenEventDoesNotExist()
        {
            var eventCommand = new Mock<IEventCommand>();
            var eventQuery = new Mock<IEventQuery>();

            eventQuery
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync((EventEntity?)null);

            var handler = new UpdateEventHandler(eventCommand.Object, eventQuery.Object);

            var req = new UpdateEventRequest
            {
                EventId = Guid.NewGuid(),
                Name = "No importa"
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(new UpdateEventCommand(req), default));
        }
    }
}
