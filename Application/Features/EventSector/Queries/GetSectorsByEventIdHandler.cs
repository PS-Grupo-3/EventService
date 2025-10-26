using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Queries;
public class GetSectorsByEventIdHandler : IRequestHandler<GetSectorsByEventIdQuery, List<EventSectorResponse>>
{
    private readonly IEventSectorQuery _eventSectorQuery;

    public GetSectorsByEventIdHandler(IEventSectorQuery eventSectorQuery)
    {
        _eventSectorQuery = eventSectorQuery;
    }

    public async Task<List<EventSectorResponse>> Handle(GetSectorsByEventIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _eventSectorQuery.GetByEventIdAsync(request.EventId, cancellationToken);

        return list.Select(s => new EventSectorResponse
        {
            EventSectorId = s.EventSectorId,
            EventId = s.EventId,
            SectorId = s.SectorId,
            Capacity = s.Capacity,
            Price = s.Price,
            Available = s.Available
        }).ToList();
    }
}