using Application.Features.Event.Queries;
using Application.Interfaces.Query;
using EventEntity = Domain.Entities.Event;
using Moq;

namespace Tests.Event.Queries
{
    public class GetAllEventsQueryTests
    {
        [Fact]
        public async Task Handle_ShouldReturnEventsList()
        {
            var eventQuery = new Mock<IEventQuery>();

            var events = new List<EventEntity>
            {
                new()
                {
                    EventId = Guid.NewGuid(),
                    Name = "Evento A",
                    UserToken = null
                },
                new()
                {
                    EventId = Guid.NewGuid(),
                    Name = "Evento B",
                    UserToken = null
                }
            };

            eventQuery
                .Setup(e => e.GetFilteredAsync(null, null, null, null, null, default))
                .ReturnsAsync(events);

            var handler = new GetFilteredEventsHandler(eventQuery.Object);

            var result = await handler.Handle(new GetFilteredEventsQuery(null, null, null, null), default);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            eventQuery.Verify(e => e.GetFilteredAsync(null, null, null, null, null, default), Times.Once);
        }
    }
}
