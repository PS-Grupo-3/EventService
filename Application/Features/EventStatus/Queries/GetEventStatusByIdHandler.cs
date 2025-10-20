using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventStatus.Queries;

public class GetEventStatusByIdHandler : IRequestHandler<GetEventStatusByIdQuery, EventStatusResponse>
{
    public Task<EventStatusResponse> Handle(GetEventStatusByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar
        throw new NotImplementedException();
    }
}
