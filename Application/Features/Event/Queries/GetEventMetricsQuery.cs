using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;

public record GetEventMetricsQuery(Guid id) : IRequest<EventMetricsResponse>;
