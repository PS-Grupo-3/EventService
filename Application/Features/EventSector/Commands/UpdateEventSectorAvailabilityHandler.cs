using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;
public class UpdateEventSectorAvailabilityHandler : IRequestHandler<UpdateEventSectorAvailabilityCommand, GenericResponse>
{
    private readonly IEventSectorCommand _command;
    private readonly IEventSectorQuery _query;

    public UpdateEventSectorAvailabilityHandler(IEventSectorCommand command, IEventSectorQuery query)
    {
        _command = command;
        _query = query;
    }

    public async Task<GenericResponse> Handle(UpdateEventSectorAvailabilityCommand req, CancellationToken ct)
    {
        var es = await _query.GetByIdAsync(req.EventSectorId, ct);
        if (es is null)
            throw new KeyNotFoundException($"No se encontró el EventSector con ID {req.EventSectorId}");
        
        await _command.UpdateAsync(es, ct);

        return new GenericResponse {
            Success = true,
            Message = $"Disponibilidad del sector actualizada a {req.Available}"
        };
    }
}
