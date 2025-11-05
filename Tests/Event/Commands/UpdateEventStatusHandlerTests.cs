using Application.Features.Event.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using EventEntity = Domain.Entities.Event;
using Moq;

namespace Tests.Event.Commands
{
    public class UpdateEventStatusHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateStatus_WhenEventExists()
        {
            var cmd = new Mock<IEventCommand>();
            var qry = new Mock<IEventQuery>();

            var ev = new EventEntity
            {
                EventId = Guid.NewGuid(),
                StatusId = 1,
                UserToken = "null"
            };

            qry.Setup(q => q.GetByIdAsync(ev.EventId, default))
                .ReturnsAsync(ev);

            var handler = new UpdateEventStatusHandler(cmd.Object, qry.Object);

            var result = await handler.Handle(
                new UpdateEventStatusCommand(ev.EventId, 2), default);

            Assert.Equal(2, ev.StatusId);

            cmd.Verify(x => x.UpdateAsync(ev, default), Times.Once);
        }
    }
}
