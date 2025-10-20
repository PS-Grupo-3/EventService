using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries;

public class EventCategoryQuery : IEventCategoryQuery
{
    private readonly AppDbContext _context;

    public EventCategoryQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EventCategories.ToListAsync(cancellationToken);
    }

    public async Task<EventCategory?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.EventCategories
            .FirstOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);
    }
}
