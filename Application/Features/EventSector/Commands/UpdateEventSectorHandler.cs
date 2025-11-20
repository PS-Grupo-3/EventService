using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;
public class UpdateEventSectorHandler : IRequestHandler<UpdateEventSectorCommand, GenericResponse>
{
    private readonly IEventSectorCommand _eventSectorCommand;
    private readonly IEventSectorQuery _eventSectorQuery;

    public UpdateEventSectorHandler(IEventSectorCommand eventSectorCommand, IEventSectorQuery eventSectorQuery)
    {
        _eventSectorCommand = eventSectorCommand;
        _eventSectorQuery = eventSectorQuery;
    }

    public async Task<GenericResponse> Handle(UpdateEventSectorCommand request, CancellationToken cancellationToken)
    {
        var existing = await _eventSectorQuery.GetByIdWithSeatsAsync(request.Request.EventSectorId, cancellationToken);
        if (existing is null)
            throw new KeyNotFoundException($"No se encontró el EventSector con ID {request.Request.EventSectorId}");

        if (request.Request.Capacity.HasValue)
            existing.Capacity = request.Request.Capacity.Value;

        if (request.Request.Price.HasValue)
            existing.Price = request.Request.Price.Value;

        if (request.Request.Available.HasValue)
            existing.Available = (bool)request.Request.Available;
        
        foreach (var seat in existing.Seats)
        {
            seat.Price = existing.Price;
        }

        if (request.Request.Capacity < 0)
            throw new ArgumentException("No se permite una capacidad menor a 0.");

        if (request.Request.Price < 0)
            throw new ArgumentException("No se permite un precio menor a 0.");

        await _eventSectorCommand.UpdateAsync(existing, cancellationToken);
        

        return new GenericResponse
        {
            Success = true,
            Message = $"EventSector con ID {existing.EventSectorId} actualizado correctamente."
        };
    }
}