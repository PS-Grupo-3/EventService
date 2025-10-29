namespace Domain.Entities;
public class EventCategory
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<CategoryType> CategoryTypes { get; set; } = new List<CategoryType>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
