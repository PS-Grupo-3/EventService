namespace Application.Models.Responses;
public class EventCategoryDetailsResponse
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<CategoryTypesResponse> CategoryTypes { get; set; } = new List<CategoryTypesResponse>();
}