using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventStatus.Queries;
public class GetAllEventStatusHandler : IRequestHandler<GetAllEventStatusQuery, List<EventStatusResponse>>
{
    private readonly IEventStatusQuery _eventStatusQuery;

    public GetAllEventStatusHandler(IEventStatusQuery eventStatusQuery)
    {
        _eventStatusQuery = eventStatusQuery;
    }
    public async Task<List<EventStatusResponse>> Handle(GetAllEventStatusQuery request, CancellationToken cancellationToken)
    {
        var eventStatuses = await _eventStatusQuery.GetAllAsync(cancellationToken);

        return eventStatuses.Select(status => new EventStatusResponse
        {
            StatusId = status.StatusId,
            Name = status.Name
        }).ToList();
    }
}
