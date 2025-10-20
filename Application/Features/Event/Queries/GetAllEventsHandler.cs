using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<EventResponse>>
    {
        public Task<List<EventResponse>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar
            throw new NotImplementedException();
        }
    }
}
