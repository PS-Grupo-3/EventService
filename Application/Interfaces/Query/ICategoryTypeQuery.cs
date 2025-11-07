using Domain.Entities;
namespace Application.Interfaces.Query;
public interface ICategoryTypeQuery
{
    Task<IEnumerable<CategoryType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryType?> GetByIdAsync(int statusId, CancellationToken cancellationToken = default);
}
