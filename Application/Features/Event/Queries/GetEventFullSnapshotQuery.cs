using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;

public record GetEventFullSnapshotQuery(Guid EventId) : IRequest<EventFullSnapshotResponse>;
