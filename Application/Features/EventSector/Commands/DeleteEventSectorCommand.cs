using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands
{
    public record DeleteEventSectorCommand(DeleteEventSectorRequest Request) : IRequest<GenericResponse>;
}
