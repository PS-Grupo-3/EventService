using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, EventDetailResponse>
    {
        public Task<EventDetailResponse> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar 
            throw new NotImplementedException();
        }
    }
}
