using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventStatus.Queries;
public record GetEventStatusByIdQuery(int StatusId) : IRequest<EventStatusResponse>;
