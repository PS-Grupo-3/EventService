using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;

public record GetAllEventCategoriesQuery() : IRequest<List<EventCategoryResponse>>;
