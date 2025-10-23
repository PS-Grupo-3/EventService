using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, GenericResponse>
    {
        private readonly IEventCommand _eventCommand;
        private readonly IEventQuery _eventQuery;

        public DeleteEventHandler(IEventCommand eventCommand, IEventQuery eventQuery)
        {
            _eventCommand = eventCommand;
            _eventQuery = eventQuery;
        }

        public async Task<GenericResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var existing = await _eventQuery.GetByIdAsync(request.Request.EventId, cancellationToken);

            if (existing is null)
                throw new KeyNotFoundException($"No se encontró el evento con ID {request.Request.EventId}");

            await _eventCommand.DeleteAsync(existing, cancellationToken);

            return new GenericResponse
            {
                Success = true,
                Message = $"Evento con ID {request.Request.EventId} eliminado correctamente."
            };
        }
    }
}
