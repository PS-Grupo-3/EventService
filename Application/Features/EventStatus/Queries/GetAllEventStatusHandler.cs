using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventStatus.Queries;

public class GetAllEventStatusHandler : IRequestHandler<GetAllEventStatusQuery, List<EventStatusResponse>>
{
    public Task<List<EventStatusResponse>> Handle(GetAllEventStatusQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar lógica para obtener todos los estados de evento
        throw new NotImplementedException();
    }
}
