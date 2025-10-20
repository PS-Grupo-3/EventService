﻿using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands;

public class EventSectorCommand : IEventSectorCommand
{
    private readonly AppDbContext _context;

    public EventSectorCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task InsertAsync(EventSector entity, CancellationToken cancellationToken = default)
    {
        await _context.EventSectors.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EventSector entity, CancellationToken cancellationToken = default)
    {
        _context.EventSectors.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EventSector entity, CancellationToken cancellationToken = default)
    {
        _context.EventSectors.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
