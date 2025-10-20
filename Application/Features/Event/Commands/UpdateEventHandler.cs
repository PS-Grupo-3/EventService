using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, GenericResponse>
    {
        public Task<GenericResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar
            throw new NotImplementedException();
        }
    }
}
