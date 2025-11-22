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
            .AsNoTracking()
            .Include(e => e.Category)
            .Include(e => e.CategoryType)
            .Include(e => e.Status)
            .Include(e => e.EventSectors)
            .ToListAsync(cancellationToken);
    }

    public async Task<Event?> GetByIdWithDetailsAsync(Guid eventId, CancellationToken ct)
    {
        return await _context.Events
            .AsNoTracking()
            
            .Include(e => e.Category)
            .Include(e => e.CategoryType)
            .Include(e => e.Status)
            
            .Include(e => e.EventSectors)
            .ThenInclude(s => s.Shape)
            
            .Include(e => e.EventSectors)
            .ThenInclude(s => s.Seats)

            .FirstOrDefaultAsync(e => e.EventId == eventId, ct);
    }

    public async Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.CategoryType)
            .Include(e => e.Status)
            .Include(e => e.EventSectors)
            .FirstOrDefaultAsync(e => e.EventId == eventId, cancellationToken);
    }

    public async Task<IEnumerable<Event>> GetFilteredAsync(Guid? eventId, int? categoryId, int? statusId, DateTime? from, DateTime? to, string? name, CancellationToken cancellationToken)
    {
        var query = _context.Events
            .Include(e => e.Category)
            .Include(e => e.CategoryType)
            .Include(e => e.Status)
            .Include(e => e.EventSectors)
            .AsQueryable();


        if (eventId.HasValue)
            query = query.Where(e => e.EventId == eventId.Value);

        if (categoryId.HasValue)
            query = query.Where(e => e.CategoryId == categoryId.Value);

        if (statusId.HasValue)
            query = query.Where(e => e.StatusId == statusId.Value);

        if (from.HasValue)
            query = query.Where(e => e.Time >= from.Value);

        if (to.HasValue)
            query = query.Where(e => e.Time <= to.Value);

        if (!string.IsNullOrWhiteSpace(name))
        {
            var lowered = name.ToLower();
            query = query.Where(e =>
                EF.Functions.Like(e.Name.ToLower(), $"%{lowered}%")
            );
        }

        return await query.ToListAsync(cancellationToken);
    }
    
    public async Task<bool> CategoryTypeBelongsToCategoryAsync(int typeId, int categoryId, CancellationToken cancellationToken)
    {
        return await _context.CategoryTypes
            .AnyAsync(ct => ct.TypeId == typeId && ct.EventCategoryId == categoryId, cancellationToken);
    }
}
