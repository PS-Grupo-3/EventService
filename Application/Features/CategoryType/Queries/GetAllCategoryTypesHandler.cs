using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventCategories.Queries
{
    public class GetAllCategoryTypesQueryHandler : IRequestHandler<GetAllCategoryTypesQuery, List<CategoryTypeDetailsResponse>>
    {
        private readonly ICategoryTypeQuery _eventTypeQuery;

        public GetAllCategoryTypesQueryHandler(ICategoryTypeQuery eventTypeQuery)
        {
            _eventTypeQuery = eventTypeQuery;
        }

        public async Task<List<CategoryTypeDetailsResponse>> Handle(GetAllCategoryTypesQuery request, CancellationToken cancellationToken)
        {
            var categoryTypes = await _eventTypeQuery.GetAllAsync();
            

            var response = categoryTypes.Select(ct => new CategoryTypeDetailsResponse
            {
                TypeId = ct.TypeId,
                Name = ct.Name,
                EventCategory = ct.EventCategory.Name
            });


            return response.ToList();
        }
    }
}