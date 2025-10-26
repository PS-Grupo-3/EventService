using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;
public class DeleteEventSectorHandler : IRequestHandler<DeleteEventSectorCommand, GenericResponse>
{
    private readonly IEventSectorCommand _eventSectorCommand;
    private readonly IEventSectorQuery _eventSectorQuery;
    
    public DeleteEventSectorHandler(IEventSectorCommand eventSectorCommand, IEventSectorQuery eventSectorQuery)
    {
        _eventSectorCommand = eventSectorCommand;
        _eventSectorQuery = eventSectorQuery;
    }
    public async Task<GenericResponse> Handle(DeleteEventSectorCommand request, CancellationToken cancellationToken)
    {
        var existing = await _eventSectorQuery.GetByIdAsync(request.Request.EventSectorId, cancellationToken);

        if (existing is null)
            throw new KeyNotFoundException($"No se encontró el EventSector con ID {request.Request.EventSectorId}");

        await _eventSectorCommand.DeleteAsync(existing, cancellationToken);

        return new GenericResponse
        {
            Success = true,
            Message = $"EventSector con ID {request.Request.EventSectorId} eliminado correctamente."
        };
    }
}
