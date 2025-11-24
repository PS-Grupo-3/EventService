using Application.Interfaces.Adapter;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;

public class GetEventFullSnapshotHandler : IRequestHandler<GetEventFullSnapshotQuery, EventFullSnapshotResponse>
{
    private readonly IEventQuery _eventQuery;
    private readonly IVenueClient _venueClient;

    public GetEventFullSnapshotHandler(IEventQuery eventQuery, IVenueClient venueClient)
    {
        _eventQuery = eventQuery;
        _venueClient = venueClient;
    }

    public async Task<EventFullSnapshotResponse> Handle(GetEventFullSnapshotQuery req, CancellationToken ct)
    {
        var e = await _eventQuery.GetByIdWithDetailsAsync(req.EventId, ct);

        if (e == null)
            throw new KeyNotFoundException($"Event {req.EventId} no encontrado.");

        var venue = await _venueClient.GetVenue(e.VenueId.ToString());
        
        return new EventFullSnapshotResponse
        {
            EventId = e.EventId,
            VenueId = e.VenueId,
            Name = e.Name,
            BannerImageUrl = e.BannerImageUrl,
            ThumbnailUrl = e.ThumbnailUrl,
            ThemeColor = e.ThemeColor,
            VenueBackgroundImageUrl = venue?.BackgroundImageUrl,
            MapUrl = venue?.MapUrl,
            Sectors = e.EventSectors.Select(s => new EventSectorFullResponse
            {
                EventSectorId = s.EventSectorId,
                Name = s.Name,
                IsControlled = s.IsControlled,
                Capacity = s.Capacity,
                Available = s.Available,
                Price = s.Price,
                Shape = new EventSectorShapeResponse
                {
                    Type = s.Shape.Type,
                    Width = s.Shape.Width,
                    Height = s.Shape.Height,
                    X = s.Shape.X,
                    Y = s.Shape.Y,
                    Rotation = s.Shape.Rotation,
                    Padding = s.Shape.Padding,
                    Opacity = s.Shape.Opacity,
                    Colour = s.Shape.Colour
                },
                Seats = s.Seats.Select(seat => new EventSeatResponse
                {
                    EventSeatId = seat.EventSeatId,
                    Row = seat.Row,
                    Column = seat.Column,
                    PosX = seat.PosX,
                    PosY = seat.PosY,
                    Price = seat.Price,
                    Available = seat.Available
                }).ToList()
            }).ToList()
        };
    }
}
