using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands
{
    public class DeleteEventSectorHandler : IRequestHandler<DeleteEventSectorCommand, GenericResponse>
    {
        public Task<GenericResponse> Handle(DeleteEventSectorCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar
            throw new NotImplementedException();
        }
    }
}
