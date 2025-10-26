using Application.Features.EventSector.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Requests;
using EventSectorEntity = Domain.Entities.EventSector;
using Moq;

namespace Tests.EventSector.Commands;
public class DeleteEventSectorHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteEventSector_WhenExists()
    {
        var command = new Mock<IEventSectorCommand>();
        var query = new Mock<IEventSectorQuery>();

        var sector = new EventSectorEntity { EventSectorId = Guid.NewGuid() };

        query
            .Setup(q => q.GetByIdAsync(sector.EventSectorId, default))
            .ReturnsAsync(sector);

        var handler = new DeleteEventSectorHandler(command.Object, query.Object);

        var req = new DeleteEventSectorRequest { EventSectorId = sector.EventSectorId };

        var result = await handler.Handle(new DeleteEventSectorCommand(req), default);

        Assert.True(result.Success);
        command.Verify(c => c.DeleteAsync(sector, default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenNotFound()
    {
        var command = new Mock<IEventSectorCommand>();
        var query = new Mock<IEventSectorQuery>();

        query
            .Setup(q => q.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((EventSectorEntity?)null);

        var handler = new DeleteEventSectorHandler(command.Object, query.Object);

        var req = new DeleteEventSectorRequest { EventSectorId = Guid.NewGuid() };

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(new DeleteEventSectorCommand(req), default));
    }
}
