using Domain.Entities;
namespace Application.Interfaces.Command;
public interface IEventSectorCommand
{
    Task InsertAsync(EventSector entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EventSector entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(EventSector entity, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid eventId, Guid sectorId, CancellationToken ct);
}
