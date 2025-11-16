using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, GenericResponse>
{
    private readonly IEventCommand _eventCommand;
    private readonly IEventQuery _eventQuery;
    private readonly IEventCategoryQuery _eventCategoryQuery;
    private readonly ICategoryTypeQuery _categoryTypeQuery;

    public UpdateEventHandler(IEventCommand eventCommand, IEventQuery eventQuery, IEventCategoryQuery eventCategoryQuery, ICategoryTypeQuery categoryTypeQuery)
    {
        _eventCommand = eventCommand;
        _eventQuery = eventQuery;
        _eventCategoryQuery = eventCategoryQuery;
        _categoryTypeQuery = categoryTypeQuery;
    }

    public async Task<GenericResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var existing = await _eventQuery.GetByIdAsync(request.Request.EventId, cancellationToken);

        if (existing is null)
            throw new KeyNotFoundException($"No se encontró el evento con ID {request.Request.EventId}");

        if (existing.StatusId == 4)
            throw new InvalidOperationException(
                $"El evento con ID {request.Request.EventId} ya está finalizado y no puede modificarse.");
        
        if (request.Request.Name is not null)
            existing.Name = request.Request.Name;
        if (request.Request.Description is not null)
            existing.Description = request.Request.Description;
        if (request.Request.Time.HasValue)
            existing.Time = request.Request.Time.Value;
        if (request.Request.BannerImageUrl is not null)
            existing.BannerImageUrl = request.Request.BannerImageUrl;
        if (request.Request.ThumbnailUrl is not null)
            existing.ThumbnailUrl = request.Request.ThumbnailUrl;
        if (request.Request.ThemeColor is not null)
            existing.ThemeColor = request.Request.ThemeColor;
        if (request.Request.StatusId.HasValue)
            existing.StatusId = request.Request.StatusId.Value;
        if (request.Request.Time < DateTime.UtcNow)
            throw new ArgumentException("Ingrese una fecha válida");

        if (request.Request.CategoryId.HasValue)
        {
            var newCategoryId = request.Request.CategoryId.Value;
            var category = await _eventCategoryQuery.GetByIdAsync(newCategoryId, cancellationToken);
            
            if (category == null)
                throw new BadRequestException400($"La Categoría con ID {newCategoryId} no existe.");

            existing.CategoryId = newCategoryId;


            if (request.Request.TypeId.HasValue)
            {
                var newTypeId = request.Request.TypeId.Value;
                var type = await _categoryTypeQuery.GetByIdAsync(newTypeId, cancellationToken);
                if (type == null)
                    throw new KeyNotFoundException($"El Tipo con ID {newTypeId} no existe.");

                
                if (type.EventCategoryId != newCategoryId)
                    throw new KeyNotFoundException($"El Tipo '{type.Name}' no pertenece a la Categoría '{category.Name}'.");

                existing.TypeId = newTypeId;
            }
            else
            {
                existing.TypeId = null;
            }
        }
        else if (request.Request.TypeId.HasValue) 
        {
            var newTypeId = request.Request.TypeId.Value;
            var type = await _categoryTypeQuery.GetByIdAsync(newTypeId, cancellationToken);
            if (type == null)
                throw new BadRequestException400($"El Tipo con ID {newTypeId} no existe.");

            // Validar que el nuevo Tipo pertenezca a la Categoría ACTUAL del evento
            if (type.EventCategoryId != existing.CategoryId)
                throw new BadRequestException400($"El Tipo '{type.Name}' no pertenece a la categoría actual del evento ('{existing.Category.Name}').");

            existing.TypeId = newTypeId;
        }

        existing.Updated = DateTime.UtcNow;

        await _eventCommand.UpdateAsync(existing, cancellationToken);

        return new GenericResponse
        {
            Success = true,
            Message = $"Evento con ID {existing.EventId} actualizado correctamente."
        };
    }
}
