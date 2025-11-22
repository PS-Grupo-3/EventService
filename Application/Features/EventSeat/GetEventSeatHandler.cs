using Application.Models.Responses;

namespace Application.Features.EventSeat;

using MediatR;

public class GetEventSeatHandler : IRequestHandler<GetEventSeatQuery, EventSeatInfoResponse>
{
    private readonly IEventSeatQuery _seatQuery;

    public GetEventSeatHandler(IEventSeatQuery seatQuery)
    {
        _seatQuery = seatQuery;
    }

    public async Task<EventSeatInfoResponse> Handle(GetEventSeatQuery request, CancellationToken cancellationToken)
    {
        var seat = await _seatQuery.GetByIdAsync(request.EventSeatId);

        if (seat == null)
            throw new Exception("Seat not found");

        return new EventSeatInfoResponse
        {
            EventSeatId = seat.EventSeatId,
            EventSectorId = seat.EventSectorId,
            PosX = seat.PosX,
            PosY = seat.PosY,
            Price = seat.Price
        };
    }
}
