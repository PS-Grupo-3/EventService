namespace Domain.Entities;
public class CategoryType
{
    public int TypeId { get; set; }
    public int EventCategoryId { get; set; }
    public string Name { get; set; } = null!;

    public EventCategory EventCategory { get; set; } = null!;
}
