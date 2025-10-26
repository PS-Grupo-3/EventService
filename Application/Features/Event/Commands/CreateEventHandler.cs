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
    public CreateEventHandler(IEventCommand eventCommand, IEventCategoryQuery eventCategoryQuery, IEventStatusQuery eventStatusQuery)
    {
        _eventCommand = eventCommand;
        _eventCategoryQuery = eventCategoryQuery;
        _eventStatusQuery = eventStatusQuery;
    }
    public async Task<EventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var category = await _eventCategoryQuery.GetByIdAsync(request.Request.CategoryId, cancellationToken);
        if (category is null)
            throw new KeyNotFoundException($"No existe una categoría con ID {request.Request.CategoryId}");

        var status = await _eventStatusQuery.GetByIdAsync(request.Request.StatusId, cancellationToken);
        if (status is null)
            throw new KeyNotFoundException($"No existe un estado con ID {request.Request.StatusId}");

        var entity = new Domain.Entities.Event
        {
            EventId = Guid.NewGuid(),
            VenueId = request.Request.VenueId,
            UserId = request.Request.UserId,
            CategoryId = request.Request.CategoryId,
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
            Status = entity.Status.Name,
            Time = entity.Time,
            Address = entity.Address
        };
    }
}
