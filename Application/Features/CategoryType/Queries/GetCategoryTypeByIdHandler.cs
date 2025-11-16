using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;
public class GetEventCategoryTypeByIdHandler : IRequestHandler<GetCategoryTypeByIdQuery, CategoryTypeDetailsResponse>
{
    private readonly ICategoryTypeQuery _eventCategoryTypeQuery;

    public GetEventCategoryTypeByIdHandler(ICategoryTypeQuery eventCategoryTypeQuery)
    {
        _eventCategoryTypeQuery = eventCategoryTypeQuery;
    }

    public async Task<CategoryTypeDetailsResponse> Handle(GetCategoryTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryType = await _eventCategoryTypeQuery.GetByIdAsync(request.TypeId, cancellationToken);

        if (categoryType is null)
            throw new ArgumentNullException($"No se encontró el tipo de categoría con el ID {request.TypeId}");

        return new CategoryTypeDetailsResponse
        {
            TypeId = categoryType.TypeId,
            Name = categoryType.Name,
            EventCategory = categoryType.EventCategory.Name
        };
    }
}
