using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public record GetEventSectorsQuery(Guid EventId) : IRequest<IEnumerable<EventSectorResponse>>;

