namespace Application.Models.Responses;
public class EventCategoryResponse
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<CategoryTypeResponse> CategoryTypes { get; set; } = new List<CategoryTypeResponse>();
}