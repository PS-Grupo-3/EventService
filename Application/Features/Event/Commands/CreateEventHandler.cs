using Application.Interfaces.Adapter;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Event.Commands;
public class CreateEventHandler : IRequestHandler<CreateEventCommand, EventResponse>
{
    private readonly IEventCommand _eventCommand;
    private readonly IEventCategoryQuery _eventCategoryQuery;
    private readonly IEventStatusQuery _eventStatusQuery;
    private readonly ICategoryTypeQuery _categoryTypeQuery;
    private readonly IVenueClient _venueClient;

    public CreateEventHandler(IEventCommand eventCommand, IEventCategoryQuery eventCategoryQuery, IEventStatusQuery eventStatusQuery, ICategoryTypeQuery categoryTypeQuery, IVenueClient venueClient)
    {
        _eventCommand = eventCommand;
        _eventCategoryQuery = eventCategoryQuery;
        _eventStatusQuery = eventStatusQuery;
        _categoryTypeQuery = categoryTypeQuery;
        _venueClient = venueClient;
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
        
        // ADAPTER
        var venueSnapshot = await _venueClient.GetVenue(request.Request.VenueId.ToString());
        if (venueSnapshot == null)
            throw new KeyNotFoundException("Venue no encontrado");
        
        foreach (var s in venueSnapshot.Sectors)
        {
            var sectorDetail = await _venueClient.GetSector(s.SectorId.ToString());
            if (sectorDetail == null)
                throw new Exception($"Sector {s.SectorId} no encontrado en VenueService");

            var eventSector = new Domain.Entities.EventSector
            {
                EventSectorId = Guid.NewGuid(),
                EventId = entity.EventId,
                VenueSectorId = s.SectorId,
                Name = s.Name,
                IsControlled = s.IsControlled,
                Capacity = s.Capacity ?? s.SeatCount
            };

            var shape = sectorDetail.Shape;
            if (shape == null)
                throw new Exception($"El sector {s.SectorId} no tiene Shape definido");

            eventSector.Shape = new EventSectorShape
            {
                EventSectorShapeId = Guid.NewGuid(),
                EventSectorId = eventSector.EventSectorId,
                Type = shape.Type,
                Width = shape.Width,
                Height = shape.Height,
                X = shape.X,
                Y = shape.Y,
                Rotation = shape.Rotation,
                Padding = shape.Padding,
                Opacity = shape.Opacity,
                Colour = shape.Colour
            };

            foreach (var seat in sectorDetail.Seats)
            {
                eventSector.Seats.Add(new EventSeat
                {
                    EventSeatId = Guid.NewGuid(),
                    EventId = entity.EventId,
                    EventSectorId = eventSector.EventSectorId,
                    Row = seat.RowNumber,
                    Column = seat.ColumnNumber,
                    PosX = seat.PosX,
                    PosY = seat.PosY,
                    Price = 0,
                    Available = true
                });
            }

            entity.EventSectors.Add(eventSector);
        }

        // ADAPTER
        
        
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