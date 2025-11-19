using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;

public record GetEventSectorByIdQuery(Guid EventSectorId) : IRequest<EventSectorFullResponse>;
