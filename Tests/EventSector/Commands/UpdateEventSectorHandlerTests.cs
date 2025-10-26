using Application.Features.EventSector.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Requests;
using EventSectorEntity = Domain.Entities.EventSector;
using Moq;

namespace Tests.EventSector.Commands;

public class UpdateEventSectorHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateEventSector_WhenExists()
    {
        var command = new Mock<IEventSectorCommand>();
        var query = new Mock<IEventSectorQuery>();

        var sector = new EventSectorEntity
        {
            EventSectorId = Guid.NewGuid(),
            Capacity = 50,
            Price = 1500,
            Available = true
        };

        query
            .Setup(q => q.GetByIdAsync(sector.EventSectorId, default))
            .ReturnsAsync(sector);

        var handler = new UpdateEventSectorHandler(command.Object, query.Object);

        var req = new UpdateEventSectorRequest
        {
            EventSectorId = sector.EventSectorId,
            Capacity = 100,
            Price = 2000,
            Available = false
        };

        var result = await handler.Handle(new UpdateEventSectorCommand(req), default);

        Assert.True(result.Success);
        Assert.Equal(100, sector.Capacity);
        Assert.Equal(2000, sector.Price);
        Assert.False(sector.Available);

        command.Verify(x => x.UpdateAsync(sector, default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenNotFound()
    {
        var command = new Mock<IEventSectorCommand>();
        var query = new Mock<IEventSectorQuery>();

        query
            .Setup(q => q.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((EventSectorEntity?)null);

        var handler = new UpdateEventSectorHandler(command.Object, query.Object);

        var req = new UpdateEventSectorRequest
        {
            EventSectorId = Guid.NewGuid(),
            Capacity = 100
        };

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(new UpdateEventSectorCommand(req), default));
    }
}
