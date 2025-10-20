using Domain.Entities;
namespace Application.Interfaces.Query;
public interface IEventCategoryQuery
{
    Task<IEnumerable<EventCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EventCategory?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
}
