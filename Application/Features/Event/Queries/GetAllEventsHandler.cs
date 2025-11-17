using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<EventResponse>>
{
    private readonly IEventQuery _eventQuery;

    public GetAllEventsHandler(IEventQuery eventQuery)
    {
        _eventQuery = eventQuery;
    }
    public async Task<List<EventResponse>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var list = await _eventQuery.GetAllAsync(cancellationToken);
        return list.Select(e => new EventResponse
        {
            EventId = e.EventId,
            Name = e.Name,
            Category = e.Category?.Name ?? "N/A",
            Status = e.Status?.Name ?? "N/A",
            Time = e.Time,
            Address = e.Address,
            BannerImageUrl = e.BannerImageUrl,
            ThumbnailUrl = e.ThumbnailUrl
        }).ToList();
    }
}
