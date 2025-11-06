using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, EventDetailResponse?>
{
    private readonly IEventQuery _eventQuery;
    public GetEventByIdHandler(IEventQuery eventQuery)
    {
        _eventQuery = eventQuery;
    }

    public async Task<EventDetailResponse?> Handle(GetEventByIdQuery request, CancellationToken ct)
    {
        var e = await _eventQuery.GetByIdAsync(request.EventId, ct);

        if (e is null)
            throw new KeyNotFoundException($"No se encontró el evento con ID {request.EventId}");

        return new EventDetailResponse
        {
            EventId = e.EventId,
            Name = e.Name,
            Description = e.Description,
            Address = e.Address,
            Time = e.Time,
            Category = e.Category?.Name ?? "N/A",
            Type = e.CategoryType?.Name ?? "N/A",
            Status = e.Status?.Name ?? "N/A",
            BannerImageUrl = e.BannerImageUrl,
            ThumbnailUrl = e.ThumbnailUrl,
            ThemeColor = e.ThemeColor,
            Sectors = e.EventSectors 
                .Select(s => new EventSectorResponse
                {
                    EventSectorId = s.EventSectorId,                    
                    Capacity = s.Capacity,
                    Price = s.Price
                }).ToList()
        };
    }
}


