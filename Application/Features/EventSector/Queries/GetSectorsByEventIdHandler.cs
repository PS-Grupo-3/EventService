using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Queries;

public class GetSectorsByEventIdHandler : IRequestHandler<GetSectorsByEventIdQuery, List<EventSectorResponse>>
{
    public Task<List<EventSectorResponse>> Handle(GetSectorsByEventIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar consulta para obtener todos los sectores de un evento
        throw new NotImplementedException();
    }
}
