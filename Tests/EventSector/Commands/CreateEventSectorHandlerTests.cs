using Application.Features.EventSector.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Requests;
using Moq;

namespace Tests.EventSector.Commands;
public class CreateEventSectorHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateEventSector_WhenEventExists()
    {
        var eventSectorCommandMock = new Mock<IEventSectorCommand>();
        var eventQueryMock = new Mock<IEventQuery>();

        var existingEvent = new Domain.Entities.Event { EventId = Guid.NewGuid() };
        eventQueryMock
            .Setup(x => x.GetByIdAsync(existingEvent.EventId, default))
            .ReturnsAsync(existingEvent);

        Domain.Entities.EventSector ? saved = null;
        eventSectorCommandMock
            .Setup(c => c.InsertAsync(It.IsAny<Domain.Entities.EventSector>(), default))
            .Callback<Domain.Entities.EventSector, CancellationToken>((e, _) => saved = e)
            .Returns(Task.CompletedTask);

        var handler = new CreateEventSectorHandler(eventSectorCommandMock.Object, eventQueryMock.Object);

        var req = new CreateEventSectorRequest
        {
            EventId = existingEvent.EventId,
            SectorId = Guid.NewGuid(),
            Capacity = 50,
            Price = 1500,
            Available = true
        };

        var result = await handler.Handle(new CreateEventSectorCommand(req), default);

        Assert.NotNull(result);
        Assert.NotNull(saved);
        Assert.Equal(req.Capacity, saved.Capacity);
        Assert.Equal(req.Price, saved.Price);

        eventSectorCommandMock.Verify(x => x.InsertAsync(It.IsAny<Domain.Entities.EventSector>(), default), Times.Once);
    }
}
