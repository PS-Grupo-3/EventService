using Domain.Entities;

namespace Application.Interfaces.Command;

public interface IEventSeatCommand
{
    Task<EventSeat?> GetByIdAsync(Guid eventSeatId, CancellationToken ct);
    Task UpdateAsync(EventSeat seat, CancellationToken ct);
}