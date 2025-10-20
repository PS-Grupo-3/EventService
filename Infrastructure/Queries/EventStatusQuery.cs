using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries;

public class EventStatusQuery : IEventStatusQuery
{
    private readonly AppDbContext _context;

    public EventStatusQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EventStatuses.ToListAsync(cancellationToken);
    }

    public async Task<EventStatus?> GetByIdAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return await _context.EventStatuses
            .FirstOrDefaultAsync(s => s.StatusId == statusId, cancellationToken);
    }
}
