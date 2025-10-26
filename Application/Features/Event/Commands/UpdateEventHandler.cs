using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, GenericResponse>
{
    private readonly IEventCommand _eventCommand;
    private readonly IEventQuery _eventQuery;

    public UpdateEventHandler(IEventCommand eventCommand, IEventQuery eventQuery)
    {
        _eventCommand = eventCommand;
        _eventQuery = eventQuery;
    }
    public async Task<GenericResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var existing = await _eventQuery.GetByIdAsync(request.Request.EventId, cancellationToken);

        if (existing is null)
            throw new KeyNotFoundException($"No se encontró el evento con ID {request.Request.EventId}");

        if (request.Request.Name is not null)
            existing.Name = request.Request.Name;
        if (request.Request.Description is not null)
            existing.Description = request.Request.Description;
        if (request.Request.Address is not null)
            existing.Address = request.Request.Address;
        if (request.Request.Time.HasValue)
            existing.Time = request.Request.Time.Value;
        if (request.Request.BannerImageUrl is not null)
            existing.BannerImageUrl = request.Request.BannerImageUrl;
        if (request.Request.ThumbnailUrl is not null)
            existing.ThumbnailUrl = request.Request.ThumbnailUrl;
        if (request.Request.ThemeColor is not null)
            existing.ThemeColor = request.Request.ThemeColor;
        if (request.Request.CategoryId.HasValue)
            existing.CategoryId = request.Request.CategoryId.Value;
        if (request.Request.StatusId.HasValue)
            existing.StatusId = request.Request.StatusId.Value;

        existing.Updated = DateTime.UtcNow;

        await _eventCommand.UpdateAsync(existing, cancellationToken);

        return new GenericResponse
        {
            Success = true,
            Message = $"Evento con ID {existing.EventId} actualizado correctamente."
        };
    }
}
