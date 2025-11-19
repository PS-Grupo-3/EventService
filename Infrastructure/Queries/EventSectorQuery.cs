using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries;

public class EventSectorQuery : IEventSectorQuery
{
    private readonly AppDbContext _context;

    public EventSectorQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventSector>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.EventSectors
            .Where(es => es.EventId == eventId)
            .ToListAsync(cancellationToken);
    }

    public async Task<EventSector?> GetByIdAsync(Guid eventSectorId, CancellationToken cancellationToken = default)
    {
        return await _context.EventSectors
            .FirstOrDefaultAsync(es => es.VenueSectorId == eventSectorId, cancellationToken);
    }
    
    public async Task<EventSector?> GetByIdWithSeatsAsync(Guid eventSectorId, CancellationToken ct)
    {
        return await _context.EventSectors
            .AsNoTracking()
            .Include(s => s.Shape)
            .Include(s => s.Seats)
            .FirstOrDefaultAsync(s => s.EventSectorId == eventSectorId, ct);
    }
}
