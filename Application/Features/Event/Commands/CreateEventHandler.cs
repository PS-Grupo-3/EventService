using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, EventResponse>
    {
        public Task<EventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar 
            throw new NotImplementedException();
        }
    }
}
