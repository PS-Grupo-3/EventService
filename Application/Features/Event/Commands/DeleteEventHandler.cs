using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, GenericResponse>
    {
        public Task<GenericResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar
            throw new NotImplementedException();
        }
    }
}
