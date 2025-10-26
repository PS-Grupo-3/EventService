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

    public async Task<IEnumerable<Event>> GetFilteredAsync(Guid? eventId = null, int? categoryId = null, int? statusId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Events
            .Include(e => e.Category)
            .Include(e => e.Status)
            .Include(e => e.EventSectors)
            .AsQueryable();

        if (eventId.HasValue)
            query = query.Where(e => e.EventId == eventId.Value);

        if (categoryId.HasValue)
            query = query.Where(e => e.CategoryId == categoryId.Value);

        if (statusId.HasValue)
            query = query.Where(e => e.StatusId == statusId.Value);

        return await query.ToListAsync(cancellationToken);
    }
}
