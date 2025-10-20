using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventStatus.Queries;

public record GetAllEventStatusQuery() : IRequest<List<EventStatusResponse>>;
