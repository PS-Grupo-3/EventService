using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Queries;

public class GetSectorByIdHandler : IRequestHandler<GetSectorByIdQuery, EventSectorResponse>
{
    public Task<EventSectorResponse> Handle(GetSectorByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implementar consulta para obtener un sector específico por su ID
        throw new NotImplementedException();
    }
}
