using Domain.Entities;
namespace Application.Interfaces.Query;
public interface IEventQuery
{
    Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default);
}
