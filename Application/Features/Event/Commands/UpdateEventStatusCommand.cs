using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public record UpdateEventStatusCommand(Guid EventId, int StatusId, string? UserId, string? UserRole) : IRequest<GenericResponse>;
