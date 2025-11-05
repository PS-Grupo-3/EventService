using Application.Features.Event.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Requests;
using Domain.Entities;
using EventEntity = Domain.Entities.Event;
using Moq;

namespace Tests.Event.Commands
{
    public class CreateEventHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateEvent_WhenDataIsValid()
        {
            var eventCommandMock = new Mock<IEventCommand>();
            var categoryQueryMock = new Mock<IEventCategoryQuery>();
            var statusQueryMock = new Mock<IEventStatusQuery>();

            var category = new EventCategory { CategoryId = 1, Name = "Rock" };
            var status = new EventStatus { StatusId = 1, Name = "Scheduled" };

            categoryQueryMock
                .Setup(q => q.GetByIdAsync(1, default))
                .ReturnsAsync(category);

            statusQueryMock
                .Setup(q => q.GetByIdAsync(1, default))
                .ReturnsAsync(status);

            EventEntity? savedEvent = null;
            eventCommandMock
                .Setup(c => c.InsertAsync(It.IsAny<EventEntity>(), default))
                .Callback<EventEntity, CancellationToken>((ev, _) => savedEvent = ev)
                .Returns(Task.CompletedTask);

            var handler = new CreateEventHandler(
                eventCommandMock.Object,
                categoryQueryMock.Object,
                statusQueryMock.Object
            );

            var request = new CreateEventRequest
            {
                VenueId = Guid.NewGuid(),
                UserToken = "token1234",
                CategoryId = 1,
                StatusId = 1,
                Name = "Mega Rock Fest",
                Description = "Concierto épico",
                Time = DateTime.UtcNow.AddMonths(1),
                Address = "Estadio Central",
                BannerImageUrl = "banner.jpg",
                ThumbnailUrl = "thumb.jpg",
                ThemeColor = "#FF0000"
            };

            var result = await handler.Handle(new CreateEventCommand(request), default);

            Assert.NotNull(result);
            Assert.Equal("Mega Rock Fest", result.Name);
            Assert.NotNull(savedEvent);
            Assert.Equal(request.Name, savedEvent.Name);
            Assert.Equal(request.CategoryId, savedEvent.CategoryId);
            Assert.Equal(request.StatusId, savedEvent.StatusId);

            eventCommandMock.Verify(c => c.InsertAsync(It.IsAny<EventEntity>(), default), Times.Once);
        }
    }
}
