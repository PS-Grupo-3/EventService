using Application.Features.EventSector.Queries;
using Application.Interfaces.Query;
using EventSectorEntity = Domain.Entities.EventSector;
using Moq;

namespace Tests.EventSector.Queries
{
    public class GetSectorsByEventIdQueryTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSectors_WhenEventIdExists()
        {
            var sectorQuery = new Mock<IEventSectorQuery>();

            var eventId = Guid.NewGuid();

            var sectors = new List<EventSectorEntity>
            {
                new() { EventSectorId = Guid.NewGuid(), EventId = eventId, Capacity = 50, Price = 1500, Available = true },
                new() { EventSectorId = Guid.NewGuid(), EventId = eventId, Capacity = 100, Price = 2500, Available = false }
            };

            sectorQuery
                .Setup(q => q.GetByEventIdAsync(eventId, default))
                .ReturnsAsync(sectors);

            var handler = new GetSectorsByEventIdHandler(sectorQuery.Object);

            var result = await handler.Handle(new GetSectorsByEventIdQuery(eventId), default);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal(eventId, r.EventId));

            sectorQuery.Verify(q => q.GetByEventIdAsync(eventId, default), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenEventHasNoSectors()
        {
            var sectorQuery = new Mock<IEventSectorQuery>();

            var eventId = Guid.NewGuid();

            sectorQuery
                .Setup(q => q.GetByEventIdAsync(eventId, default))
                .ReturnsAsync(new List<EventSectorEntity>());

            var handler = new GetSectorsByEventIdHandler(sectorQuery.Object);

            var result = await handler.Handle(new GetSectorsByEventIdQuery(eventId), default);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
