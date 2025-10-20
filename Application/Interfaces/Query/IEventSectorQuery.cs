using Domain.Entities;
namespace Application.Interfaces.Query;
public interface IEventSectorQuery
{
    Task<IEnumerable<EventSector>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<EventSector?> GetByIdAsync(Guid eventSectorId, CancellationToken cancellationToken = default);
}
