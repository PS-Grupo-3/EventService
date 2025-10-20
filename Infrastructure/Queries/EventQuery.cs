using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries;

public class EventQuery : IEventQuery
{
    private readonly AppDbContext _context;

    public EventQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.Status)
            .Include(e => e.EventSectors)
            .ToListAsync(cancellationToken);
    }

    public async Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.Status)
            .Include(e => e.EventSectors)
            .FirstOrDefaultAsync(e => e.EventId == eventId, cancellationToken);
    }
}
