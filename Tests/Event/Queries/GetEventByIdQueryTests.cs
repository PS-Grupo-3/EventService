using Application.Features.Event.Queries;
using Application.Interfaces.Query;
using EventEntity = Domain.Entities.Event;
using Moq;

namespace Tests.Event.Queries
{
    public class GetEventByIdQueryTests
    {
        [Fact]
        public async Task Handle_ShouldReturnEventDetail_WhenEventExists()
        {
            var eventQuery = new Mock<IEventQuery>();

            var ev = new EventEntity
            {
                EventId = Guid.NewGuid(),
                Name = "Festival Final",
                Description = "El último show",
                Address = "Estadio",
                Time = DateTime.UtcNow,
                Category = new Domain.Entities.EventCategory { Name = "Rock" },
                Status = new Domain.Entities.EventStatus { Name = "Active" }
            };

            eventQuery
                .Setup(e => e.GetByIdAsync(ev.EventId, default))
                .ReturnsAsync(ev);

            var handler = new GetEventByIdHandler(eventQuery.Object);

            var result = await handler.Handle(new GetEventByIdQuery(ev.EventId), default);

            Assert.NotNull(result);
            Assert.Equal(ev.EventId, result.EventId);
            Assert.Equal(ev.Name, result.Name);

            eventQuery.Verify(e => e.GetByIdAsync(ev.EventId, default), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEventDoesNotExist()
        {
            var eventQuery = new Mock<IEventQuery>();

            eventQuery
                .Setup(e => e.GetByIdAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync((EventEntity?)null);

            var handler = new GetEventByIdHandler(eventQuery.Object);

            var result = await handler.Handle(new GetEventByIdQuery(Guid.NewGuid()), default);

            Assert.Null(result);
        }
    }
}
