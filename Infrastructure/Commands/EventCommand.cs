using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands;

public class EventCommand : IEventCommand
{
    private readonly AppDbContext _context;

    public EventCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task InsertAsync(Event entity, CancellationToken cancellationToken = default)
    {
        await _context.Events.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Event entity, CancellationToken cancellationToken = default)
    {
        _context.Events.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Event entity, CancellationToken cancellationToken = default)
    {
        _context.Events.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task LoadCategoryTypeAsync(Event evt, CancellationToken ct)
    {
        await _context.Entry(evt)
            .Reference(e => e.CategoryType)
            .LoadAsync(ct);
    }

}
