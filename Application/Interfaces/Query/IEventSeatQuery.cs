using Domain.Entities;

public interface IEventSeatQuery
{
    Task<EventSeat?> GetByIdAsync(Guid seatId);
}