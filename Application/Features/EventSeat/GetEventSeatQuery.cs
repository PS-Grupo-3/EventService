using Application.Models.Responses;

namespace Application.Features.EventSeat;

using MediatR;

public record GetEventSeatQuery(Guid EventSeatId) : IRequest<EventSeatInfoResponse>;
