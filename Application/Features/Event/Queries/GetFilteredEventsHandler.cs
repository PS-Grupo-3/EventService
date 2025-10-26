using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public class GetFilteredEventsHandler : IRequestHandler<GetFilteredEventsQuery, List<EventResponse>>
{
    private readonly IEventQuery _eventQuery;
    public GetFilteredEventsHandler(IEventQuery eventQuery)
    {
        _eventQuery = eventQuery;
    }

    public async Task<List<EventResponse>> Handle(GetFilteredEventsQuery request, CancellationToken ct)
    {
        var list = await _eventQuery.GetFilteredAsync(null, request.CategoryId, request.StatusId, ct);

        return list.Select(e => new EventResponse
        {
            EventId = e.EventId,
            Name = e.Name,
            Category = e.Category?.Name ?? "N/A",
            Status = e.Status?.Name ?? "N/A",
            Time = e.Time,
            Address = e.Address
        }).ToList();
    }
}
