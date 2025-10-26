using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Queries;
public record GetSectorsByEventIdQuery(Guid EventId) : IRequest<List<EventSectorResponse>>;