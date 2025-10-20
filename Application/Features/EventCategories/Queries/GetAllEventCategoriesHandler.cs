using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;

public class GetAllEventCategoriesHandler : IRequestHandler<GetAllEventCategoriesQuery, List<EventCategoryResponse>>
{
    public Task<List<EventCategoryResponse>> Handle(GetAllEventCategoriesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar 
        throw new NotImplementedException();
    }
}
