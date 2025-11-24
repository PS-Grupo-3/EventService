namespace Application.Features.EventSector.Commands;

using Application.Features.EventSector.Commands;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

public class ReleaseFreeSectorHandler : IRequestHandler<ReleaseFreeSectorCommand, GenericResponse>
{
    private readonly IEventSectorQuery _query;
    private readonly IEventSectorCommand _command;

    public ReleaseFreeSectorHandler(IEventSectorQuery query, IEventSectorCommand command)
    {
        _query = query;
        _command = command;
    }

    public async Task<GenericResponse> Handle(ReleaseFreeSectorCommand request, CancellationToken cancellationToken)
    {
        var sector = await _query.GetByIdAsync(request.EventSectorId, cancellationToken);
        if (sector is null)
            return new GenericResponse { Success = false, Message = "Sector no encontrado." };

        if (sector.IsControlled)
            return new GenericResponse { Success = false, Message = "Este sector usa asientos, no capacidad libre." };

        sector.Capacity += 1;

        await _command.UpdateAsync(sector, cancellationToken);

        return new GenericResponse
        {
            Success = true,
            Message = "Liberación aplicada correctamente."
        };
    }
}
