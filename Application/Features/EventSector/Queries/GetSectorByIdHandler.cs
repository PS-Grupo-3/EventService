using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Queries;
public class GetSectorByIdHandler : IRequestHandler<GetSectorByIdQuery, EventSectorResponse>
{
    private readonly IEventSectorQuery _eventSectorQuery;

    public GetSectorByIdHandler(IEventSectorQuery eventSectorQuery)
    {
        _eventSectorQuery = eventSectorQuery;
    }

    public async Task<EventSectorResponse> Handle(GetSectorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _eventSectorQuery.GetByIdAsync(request.EventSectorId, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"No se encontró el EventSector con ID {request.EventSectorId}");

        return new EventSectorResponse
        {
            EventSectorId = entity.EventSectorId,
            EventId = entity.EventId,
            SectorId = entity.VenueSectorId,
            Capacity = entity.Capacity,
            Price = entity.Price,
            Available = entity.Available
        };
    }
}