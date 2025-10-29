using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;
public class GetAllEventCategoriesHandler : IRequestHandler<GetAllEventCategoriesQuery, List<EventCategoryResponse>>
{
    private readonly IEventCategoryQuery _eventCategoryQuery;
    public GetAllEventCategoriesHandler(IEventCategoryQuery eventCategoryQuery)
    {
        _eventCategoryQuery = eventCategoryQuery;
    }

    public async Task<List<EventCategoryResponse>> Handle(GetAllEventCategoriesQuery request, CancellationToken cancellationToken)
    {
        var eventCategories = await _eventCategoryQuery.GetAllAsync(cancellationToken);

        return eventCategories.Select(categories => new EventCategoryResponse
        {
            CategoryId = categories.CategoryId,
            Name = categories.Name,
            CategoryTypes = categories.CategoryTypes
            .OrderBy(t => t.TypeId)
            .Select(t => new CategoryTypeResponse
            {
                TypeId = t.TypeId,
                Name = t.Name
            })
        }).ToList();
    }
}
