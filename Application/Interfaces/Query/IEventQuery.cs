using Domain.Entities;
namespace Application.Interfaces.Query;
public interface IEventQuery
{
    Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Event>> GetFilteredAsync(Guid? eventId = null, int? categoryId = null, int? statusId = null, CancellationToken cancellationToken = default);
}
