using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public record UpdateEventCommand(Guid EventId, UpdateEventRequest Request, string? UserId, string? UserRole) : IRequest<GenericResponse>;
