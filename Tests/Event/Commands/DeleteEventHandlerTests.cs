using Application.Features.Event.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Requests;
using EventEntity = Domain.Entities.Event;
using Moq;

namespace Tests.Event.Commands
{
    public class DeleteEventHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteEvent_WhenEventExists()
        {
            var eventCommand = new Mock<IEventCommand>();
            var eventQuery = new Mock<IEventQuery>();

            var ev = new EventEntity { EventId = Guid.NewGuid() };

            eventQuery
                .Setup(x => x.GetByIdAsync(ev.EventId, default))
                .ReturnsAsync(ev);

            var handler = new DeleteEventHandler(eventCommand.Object, eventQuery.Object);

            var req = new DeleteEventRequest { EventId = ev.EventId };

            var result = await handler.Handle(new DeleteEventCommand(req), default);

            Assert.True(result.Success);
            eventCommand.Verify(x => x.DeleteAsync(ev, default), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenEventDoesNotExist()
        {
            var eventCommand = new Mock<IEventCommand>();
            var eventQuery = new Mock<IEventQuery>();

            eventQuery
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync((EventEntity?)null);

            var handler = new DeleteEventHandler(eventCommand.Object, eventQuery.Object);

            var req = new DeleteEventRequest { EventId = Guid.NewGuid() };

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(new DeleteEventCommand(req), default));
        }
    }
}
