using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public class GetEventSectorsHandler : IRequestHandler<GetEventSectorsQuery, IEnumerable<EventSectorResponse>>
{
    private readonly IEventSectorQuery _eventSectorQuery;

    public GetEventSectorsHandler(IEventSectorQuery eventSectorQuery)
    {
        _eventSectorQuery = eventSectorQuery;
    }

    public async Task<IEnumerable<EventSectorResponse>> Handle(GetEventSectorsQuery request, CancellationToken cancellationToken)
    {
        var sectors = await _eventSectorQuery.GetByEventIdAsync(request.EventId, cancellationToken);

        return sectors.Select(s => new EventSectorResponse
        {
            EventSectorId = s.EventSectorId,
            EventId = s.EventId,
            SectorId = s.VenueSectorId,
            Capacity = s.Capacity,
            Price = s.Price
        }).ToList();
    }

}

