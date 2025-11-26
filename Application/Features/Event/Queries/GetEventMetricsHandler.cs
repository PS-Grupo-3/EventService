using Application.Interfaces.Adapter;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;

public class GetEventMetricsHandler : IRequestHandler<GetEventMetricsQuery, EventMetricsResponse>
{
    private readonly IEventQuery _eventQuery;
    private readonly IVenueClient _venueClient;

    public GetEventMetricsHandler(IEventQuery eventQuery, IVenueClient venueClient)
    { 
        _eventQuery = eventQuery;
        _venueClient = venueClient;
    }

    public async Task<EventMetricsResponse> Handle(GetEventMetricsQuery req, CancellationToken ct)
    {
        var e = await _eventQuery.GetByIdWithDetailsAsync(req.id, ct);

        if (e == null)
            throw new KeyNotFoundException($"Event {req.id} no encontrado.");

        var venue = await _venueClient.GetVenue(e.VenueId.ToString());

        int totalSeats = e.EventSectors.Sum(s => s.Seats.Count);
        int soldSeats = e.EventSectors.Sum(s => s.Seats.Count(seat => !seat.Available));
        int availableSeats = totalSeats - soldSeats;
        double occupancyRate = totalSeats > 0
            ? Math.Round((double)soldSeats / totalSeats * 100, 2)
            : 0;

        decimal totalRevenue = e.EventSectors.Sum(s => s.Seats
            .Where(seat => !seat.Available)
            .Sum(seat => seat.Price));

        var sectorsMetrics = e.EventSectors
            .Where(s => s.Available)
            .Select(s => new EventSectorMetricsResponse
            {
                SectorId = s.EventSectorId,
                Name = s.Name,
                TotalSeats = s.Seats.Count,
                SoldSeats = s.Seats.Count(seat => !seat.Available),
                AvailableSeats = s.Seats.Count(seat => seat.Available),
                Renueve = s.Seats.Where(seat => !seat.Available).Sum(seat => seat.Price),
                OccupancyRate = s.Seats.Count > 0
                    ? Math.Round((double)s.Seats.Count(seat => !seat.Available) / s.Seats.Count * 100, 2)
                    : 0
            }).ToList();

        var response = new EventMetricsResponse
        {
            EventId = e.EventId,
            VenueId = e.VenueId,
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
            VenueBackgroundImageUrl = venue.BackgroundImageUrl,
            MapUrl = venue.MapUrl,
            TotalSeats = totalSeats,
            SoldSeats = soldSeats,
            AvailableSeats = availableSeats,
            OcupancyRate = occupancyRate,
            TotalRenueve = totalRevenue,
            Sectors = sectorsMetrics
        };

        return response;
    }

}