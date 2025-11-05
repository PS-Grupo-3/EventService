using Domain.Entities;
namespace Application.Interfaces.Query;
public interface IEventQuery
{
    Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Event>> GetFilteredAsync(Guid? eventId, int? categoryId, int? statusId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
    Task<bool> CategoryTypeBelongsToCategoryAsync(int typeId, int categoryId, CancellationToken cancellationToken);
}
