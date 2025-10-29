using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;
public class GetEventCategoryByIdHandler : IRequestHandler<GetEventCategoryByIdQuery, EventCategoryResponse>
{
    private readonly IEventCategoryQuery _eventCategoryQuery;

    public GetEventCategoryByIdHandler(IEventCategoryQuery eventCategoryQuery)
    {
        _eventCategoryQuery = eventCategoryQuery;
    }
    public async Task<EventCategoryResponse> Handle(GetEventCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _eventCategoryQuery.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
            throw new ArgumentNullException($"No se encontró la categoria con el ID {request.CategoryId}");

        return new EventCategoryResponse
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            CategoryTypes = category.CategoryTypes.Select(ct => new CategoryTypeResponse
            {
                TypeId = ct.TypeId,
                Name = ct.Name
            })
        };
    }
}
