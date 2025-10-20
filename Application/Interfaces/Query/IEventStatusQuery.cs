using Domain.Entities;
namespace Application.Interfaces.Query;
public interface IEventStatusQuery
{
    Task<IEnumerable<EventStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EventStatus?> GetByIdAsync(int statusId, CancellationToken cancellationToken = default);
}
