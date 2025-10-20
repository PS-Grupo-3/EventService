using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;

public class GetEventCategoryByIdHandler : IRequestHandler<GetEventCategoryByIdQuery, EventCategoryResponse>
{
    public Task<EventCategoryResponse> Handle(GetEventCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar
        throw new NotImplementedException();
    }
}
