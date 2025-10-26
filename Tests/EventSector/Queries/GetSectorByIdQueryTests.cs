using Application.Features.EventSector.Queries;
using Application.Interfaces.Query;
using EventSectorEntity = Domain.Entities.EventSector;
using Moq;

namespace Tests.EventSector.Queries
{
    public class GetSectorByIdQueryTests
    {
        [Fact]
        public async Task Handle_ShouldReturnEventSector_WhenExists()
        {
            var sectorQuery = new Mock<IEventSectorQuery>();

            var sector = new EventSectorEntity
            {
                EventSectorId = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                SectorId = Guid.NewGuid(),
                Capacity = 100,
                Price = 2000,
                Available = true
            };

            sectorQuery
                .Setup(q => q.GetByIdAsync(sector.EventSectorId, default))
                .ReturnsAsync(sector);

            var handler = new GetSectorByIdHandler(sectorQuery.Object);

            var result = await handler.Handle(new GetSectorByIdQuery(sector.EventSectorId), default);

            Assert.NotNull(result);
            Assert.Equal(sector.EventSectorId, result.EventSectorId);
            Assert.Equal(sector.Capacity, result.Capacity);

            sectorQuery.Verify(q => q.GetByIdAsync(sector.EventSectorId, default), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFound_WhenNotFound()
        {
            var sectorQuery = new Mock<IEventSectorQuery>();

            sectorQuery
                .Setup(q => q.GetByIdAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync((EventSectorEntity?)null);

            var handler = new GetSectorByIdHandler(sectorQuery.Object);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(new GetSectorByIdQuery(Guid.NewGuid()), default));
        }

    }
}
