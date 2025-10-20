using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands
{
    public class CreateEventSectorHandler : IRequestHandler<CreateEventSectorCommand, GenericResponse>
    {
        public Task<GenericResponse> Handle(CreateEventSectorCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar
            throw new NotImplementedException();
        }
    }
}
