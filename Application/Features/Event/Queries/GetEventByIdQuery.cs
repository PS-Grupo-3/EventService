using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries
{
    public record GetEventByIdQuery(Guid EventId) : IRequest<EventDetailResponse>;
}
