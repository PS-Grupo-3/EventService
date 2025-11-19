using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;

public class GetEventSectorByIdHandler : IRequestHandler<GetEventSectorByIdQuery, EventSectorFullResponse>
{
    private readonly IEventSectorQuery _query;

    public GetEventSectorByIdHandler(IEventSectorQuery query)
    {
        _query = query;
    }

    public async Task<EventSectorFullResponse> Handle(GetEventSectorByIdQuery req, CancellationToken ct)
    {
        var s = await _query.GetByIdWithSeatsAsync(req.EventSectorId, ct);

        if (s == null)
            throw new KeyNotFoundException($"Sector {req.EventSectorId} no encontrado.");

        return new EventSectorFullResponse
        {
            EventSectorId = s.EventSectorId,
            Name = s.Name,
            IsControlled = s.IsControlled,
            Capacity = s.Capacity,
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
            Seats = s.Seats.Select(x => new EventSeatResponse
            {
                EventSeatId = x.EventSeatId,
                Row = x.Row,
                Column = x.Column,
                PosX = x.PosX,
                PosY = x.PosY,
                Price = x.Price,
                Available = x.Available
            }).ToList()
        };
    }
}
