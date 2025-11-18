using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public class CreateEventHandler : IRequestHandler<CreateEventCommand, EventResponse>
{
    private readonly IEventCommand _eventCommand;
    private readonly IEventCategoryQuery _eventCategoryQuery;
    private readonly IEventStatusQuery _eventStatusQuery;
    private readonly ICategoryTypeQuery _categoryTypeQuery;
    private readonly IVenueClient _venueClient;

    public CreateEventHandler(IEventCommand eventCommand, IEventCategoryQuery eventCategoryQuery, IEventStatusQuery eventStatusQuery, ICategoryTypeQuery categoryTypeQuery)
    {
        _eventCommand = eventCommand;
        _eventCategoryQuery = eventCategoryQuery;
        _eventStatusQuery = eventStatusQuery;
        _categoryTypeQuery = categoryTypeQuery;
    }

    public async Task<EventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var category = await _eventCategoryQuery.GetByIdAsync(request.Request.CategoryId, cancellationToken);
        var categoryType = await _categoryTypeQuery.GetByIdAsync(request.Request.TypeId, cancellationToken);

        if (category is null)
            throw new KeyNotFoundException($"No existe una categoría con ID {request.Request.CategoryId}");
        if (categoryType is null)
            throw new KeyNotFoundException($"No existe un tipo de categoría con ID {request.Request.TypeId}");

        if (categoryType.EventCategoryId != category.CategoryId)
            throw new KeyNotFoundException("El tipo de categoría no pertenece a la categoría elegida");

        var status = await _eventStatusQuery.GetByIdAsync(request.Request.StatusId, cancellationToken);
        if (status is null)
            throw new KeyNotFoundException($"No existe un estado con ID {request.Request.StatusId}");

        if (request.UserRole == "Current")
            throw new UnauthorizedAccessException("Los usuarios comunes no pueden crear eventos.");

        if (request.Request.Time < DateTime.UtcNow)
            throw new ArgumentException("Ingrese una fecha válida");

        var entity = new Domain.Entities.Event
        {
            EventId = Guid.NewGuid(),
            VenueId = request.Request.VenueId,
            CategoryId = request.Request.CategoryId,
            TypeId = request.Request.TypeId,
            StatusId = request.Request.StatusId,
            Name = request.Request.Name,
            Description = request.Request.Description,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Time = request.Request.Time,
            Address = request.Request.Address,
            BannerImageUrl = request.Request.BannerImageUrl,
            ThumbnailUrl = request.Request.ThumbnailUrl,
            ThemeColor = request.Request.ThemeColor
        };

        await _eventCommand.InsertAsync(entity, cancellationToken);

        return new EventResponse
        {
            EventId = entity.EventId,
            Name = entity.Name,
            Category = entity.Category.Name,
            CategoryType = entity.CategoryType.Name,
            Status = entity.Status.Name,
            Time = entity.Time,
            Address = entity.Address,
            BannerImageUrl = entity.BannerImageUrl,
            ThumbnailUrl = entity.ThumbnailUrl
        };
    }
}