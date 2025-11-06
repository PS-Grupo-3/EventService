using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries;
public record GetAllCategoryTypesQuery() : IRequest<List<CategoryTypeResponse>>;
