using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands
{
    public record UpdateEventCommand(UpdateEventRequest Request) : IRequest<GenericResponse>;
}
