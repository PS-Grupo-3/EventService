using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Commands;
public record CreateEventCommand(CreateEventRequest Request, string? UserId, string? UserRole) : IRequest<EventResponse>;
