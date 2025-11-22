using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class EventSeatQuery : IEventSeatQuery
{
    private readonly AppDbContext _context;

    public EventSeatQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EventSeat?> GetByIdAsync(Guid seatId)
    {
        return await _context.EventSeats
            .FirstOrDefaultAsync(x => x.EventSeatId == seatId);
    }


}