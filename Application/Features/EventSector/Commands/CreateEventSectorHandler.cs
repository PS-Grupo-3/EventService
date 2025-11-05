using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;
public class CreateEventSectorHandler : IRequestHandler<CreateEventSectorCommand, EventSectorResponse>
{
    private readonly IEventSectorCommand _eventSectorCommand;
    private readonly IEventQuery _eventQuery;        
    
    public CreateEventSectorHandler(IEventSectorCommand eventSectorCommand, IEventQuery eventQuery)
    {
        _eventSectorCommand = eventSectorCommand;
        _eventQuery = eventQuery;
    }

    public async Task<EventSectorResponse> Handle(CreateEventSectorCommand request, CancellationToken cancellationToken)
    {
        var eventExists = await _eventQuery.GetByIdAsync(request.Request.EventId);

        if (eventExists is null)
            throw new KeyNotFoundException($"No existe un evento con ID {request.Request.EventId}");

        var alreadyAssigned = await _eventSectorCommand.ExistsAsync(
            request.Request.EventId,
            request.Request.SectorId,
            cancellationToken);

        if (alreadyAssigned)
            throw new InvalidOperationException(
                $"El sector {request.Request.SectorId} ya está asignado al evento {request.Request.EventId}");

        var entity = new Domain.Entities.EventSector
        {
            EventSectorId = Guid.NewGuid(),
            EventId = request.Request.EventId,
            SectorId = request.Request.SectorId,
            Capacity = request.Request.Capacity,
            Price = request.Request.Price,
            Available = request.Request.Available
        };

        await _eventSectorCommand.InsertAsync(entity, cancellationToken);

        return new EventSectorResponse
        {
            EventSectorId = entity.EventSectorId,
            EventId = entity.EventId,
            SectorId = entity.SectorId,
            Capacity = entity.Capacity,
            Price = entity.Price,
            Available = entity.Available
        };
    }

}
