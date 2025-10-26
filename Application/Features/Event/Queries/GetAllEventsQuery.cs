using Application.Models.Responses;
using MediatR;

namespace Application.Features.Event.Queries;
public record GetAllEventsQuery() : IRequest<List<EventResponse>>;
