using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public class UpdateEventStatusHandler : IRequestHandler<UpdateEventStatusCommand, GenericResponse>
{
    private readonly IEventCommand _eventCommand;
    private readonly IEventQuery _eventQuery;

    public UpdateEventStatusHandler(IEventCommand eventCommand, IEventQuery eventQuery)
    {
        _eventCommand = eventCommand;
        _eventQuery = eventQuery;
    }

    public async Task<GenericResponse> Handle(UpdateEventStatusCommand request, CancellationToken cancellationToken)
    {
        var existing = await _eventQuery.GetByIdAsync(request.EventId, cancellationToken);
        if (existing is null)
            throw new KeyNotFoundException($"Evento con ID {request.EventId} no encontrado.");

        existing.StatusId = request.StatusId;
        existing.Updated = DateTime.UtcNow;

        await _eventCommand.UpdateAsync(existing, cancellationToken);

        return new GenericResponse
        {
            Success = true,
            Message = $"Estado del evento actualizado a {request.StatusId}"
        };
    }
}


