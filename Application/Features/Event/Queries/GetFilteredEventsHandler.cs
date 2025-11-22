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
        if (request.From.HasValue && request.To.HasValue && request.From > request.To)
            throw new ArgumentException("From date must be earlier than To date.");
        
        var list = await _eventQuery.GetFilteredAsync(null, request.CategoryId, request.StatusId, request.From, request.To, request.Name, ct);

        return list.Select(e => new EventResponse
        {
            EventId = e.EventId,
            VenueId = e.VenueId,
            Name = e.Name,
            Category = e.Category?.Name ?? "N/A",
            CategoryType = e.CategoryType?.Name ?? "N/A",
            Status = e.Status?.Name ?? "N/A",
            Time = e.Time,
            Address = e.Address,
            BannerImageUrl = e.BannerImageUrl,
            ThumbnailUrl = e.ThumbnailUrl
        }).ToList();
    }
}
