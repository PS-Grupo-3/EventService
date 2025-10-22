using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventStatus.Queries;

public class GetEventStatusByIdHandler : IRequestHandler<GetEventStatusByIdQuery, EventStatusResponse>
{
    private readonly IEventStatusQuery _eventStatusQuery;

    public GetEventStatusByIdHandler(IEventStatusQuery eventStatusQuery)
    {
        _eventStatusQuery = eventStatusQuery;
    }

    public async Task<EventStatusResponse> Handle(GetEventStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var status = await _eventStatusQuery.GetByIdAsync(request.StatusId, cancellationToken);

        if (status is null)        
            throw new KeyNotFoundException($"No se encontró el estado con el ID {request.StatusId}");

        return new EventStatusResponse
        {
            StatusId = request.StatusId,
            Name = status.Name
        };
    }
}
