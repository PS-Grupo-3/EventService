using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries
{
    public class GetAllCategoryTypesQueryHandler : IRequestHandler<GetAllCategoryTypesQuery, List<CategoryTypeResponse>>
    {
        private readonly ICategoryTypeQuery _eventTypeQuery;

        public GetAllCategoryTypesQueryHandler(ICategoryTypeQuery eventTypeQuery)
        {
            _eventTypeQuery = eventTypeQuery;
        }

        public async Task<List<CategoryTypeResponse>> Handle(GetAllCategoryTypesQuery request, CancellationToken cancellationToken)
        {
            var categoryTypes = await _eventTypeQuery.GetAllAsync();

            var response = categoryTypes.Select(ct => new CategoryTypeResponse
            {
                TypeId = ct.TypeId,
                Name = ct.Name
            });


            return response.ToList();
        }
    }
}