using Application.Features.EventSector.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Moq;

namespace Tests.EventSector.Commands
{
    public class UpdateEventSectorAvailabilityHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateAvailability()
        {
            var cmd = new Mock<IEventSectorCommand>();
            var qry = new Mock<IEventSectorQuery>();

            var sector = new Domain.Entities.EventSector
            {
                EventSectorId = Guid.NewGuid(),
                Available = false
            };

            qry.Setup(x => x.GetByIdAsync(sector.EventSectorId, default))
                .ReturnsAsync(sector);

            var handler = new UpdateEventSectorAvailabilityHandler(cmd.Object, qry.Object);

            var result = await handler.Handle(
                new UpdateEventSectorAvailabilityCommand(sector.EventSectorId, true), default);

            Assert.True(sector.Available);
            cmd.Verify(x => x.UpdateAsync(sector, default), Times.Once);
        }
    }
}
