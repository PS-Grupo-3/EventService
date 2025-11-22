using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public record GetFilteredEventsQuery(int? CategoryId, int? StatusId, DateTime? From, DateTime? To, string? Name) : IRequest<List<EventResponse>>;
