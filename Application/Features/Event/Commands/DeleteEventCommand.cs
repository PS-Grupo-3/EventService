using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public record DeleteEventCommand(DeleteEventRequest Request) : IRequest<GenericResponse>;
