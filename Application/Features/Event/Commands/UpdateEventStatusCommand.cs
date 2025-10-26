using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public record UpdateEventStatusCommand(Guid EventId, int StatusId) : IRequest<GenericResponse>;
