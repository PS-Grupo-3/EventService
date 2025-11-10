using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;
public record UpdateEventSectorCommand(UpdateEventSectorRequest Request, string? UserId, string? UserRole) : IRequest<GenericResponse>;

