using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;
public record GetEventCategoryByIdQuery(int CategoryId) : IRequest<EventCategoryResponse>;
