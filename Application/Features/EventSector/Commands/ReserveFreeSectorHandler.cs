using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;

public class ReserveFreeSectorHandler : IRequestHandler<ReserveFreeSectorCommand, GenericResponse>
{
    private readonly IEventSectorQuery _query;
    private readonly IEventSectorCommand _command;

    public ReserveFreeSectorHandler(IEventSectorQuery query, IEventSectorCommand command)
    {
        _query = query;
        _command = command;
    }

    public async Task<GenericResponse> Handle(ReserveFreeSectorCommand request, CancellationToken cancellationToken)
    {
        var sector = await _query.GetByIdAsync(request.EventSectorId, cancellationToken);
        if (sector is null)
            return new GenericResponse { Success = false, Message = "Sector no encontrado." };

        if (sector.IsControlled)
            return new GenericResponse { Success = false, Message = "Este sector usa asientos, no capacidad libre." };

        if (sector.Capacity <= 0)
            return new GenericResponse { Success = false, Message = "No hay más capacidad disponible." };

        sector.Capacity -= 1;

        await _command.UpdateAsync(sector, cancellationToken);

        return new GenericResponse
        {
            Success = true,
            Message = "Reserva aplicada correctamente."
        };
    }
}