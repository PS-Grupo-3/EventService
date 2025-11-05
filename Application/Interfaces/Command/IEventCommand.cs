using Domain.Entities;
namespace Application.Interfaces.Command;
public interface IEventCommand
{
    Task InsertAsync(Event entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Event entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Event entity, CancellationToken cancellationToken = default);
    Task LoadCategoryTypeAsync(Event evt, CancellationToken ct);

}
