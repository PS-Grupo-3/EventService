using Application.Interfaces.Command;
using MediatR;

namespace Application.Features.EventSeat;

public class UpdateSeatStatusHandler : IRequestHandler<UpdateSeatStatusCommand, bool>
{
    private readonly IEventSeatCommand _command;

    public UpdateSeatStatusHandler(IEventSeatCommand command)
    {
        _command = command;
    }

    public async Task<bool> Handle(UpdateSeatStatusCommand req, CancellationToken ct)
    {
        var seat = await _command.GetByIdAsync(req.EventSeatId, ct);
        if (seat == null)
            throw new KeyNotFoundException("Asiento no encontrado");

        if (req.Request.Available.HasValue)
            seat.Available = req.Request.Available.Value;

        await _command.UpdateAsync(seat, ct);
        return true;
    }
}
