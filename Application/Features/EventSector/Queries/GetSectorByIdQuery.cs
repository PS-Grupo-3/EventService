using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Queries;
public record GetSectorByIdQuery(Guid EventSectorId) : IRequest<EventSectorResponse>;

