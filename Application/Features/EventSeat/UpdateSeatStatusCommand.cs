using Application.Models.Requests;
using MediatR;

namespace Application.Features.EventSeat;

public record UpdateSeatStatusCommand(Guid EventSeatId, UpdateSeatStatusRequest Request) : IRequest<bool>;
