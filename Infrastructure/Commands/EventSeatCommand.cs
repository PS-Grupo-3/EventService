using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands;

public class EventSeatCommand : IEventSeatCommand
{
    private readonly AppDbContext _context;

    public EventSeatCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EventSeat?> GetByIdAsync(Guid eventSeatId, CancellationToken ct)
    {
        return await _context.EventSeats
            .FirstOrDefaultAsync(x => x.EventSeatId == eventSeatId, ct);
    }

    public async Task UpdateAsync(EventSeat seat, CancellationToken ct)
    {
        _context.EventSeats.Update(seat);
        await _context.SaveChangesAsync(ct);
    }
}