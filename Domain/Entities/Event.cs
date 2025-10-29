namespace Domain.Entities;
public class Event
{
    public Guid EventId { get; set; }
    public Guid VenueId { get; set; }
    public required string UserToken { get; set; }
    public int CategoryId { get; set; } 
    public int TypeId { get; set; }
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }        
    public DateTime Time { get; set; }
    public string Address { get; set; } = null!;        
    public string BannerImageUrl { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public string ThemeColor { get; set; } = "#FFFFFF";

    public EventCategory Category { get; set; } = null!;
    public CategoryType CategoryType { get; set; } = null!;
    public EventStatus Status { get; set; } = null!;
    public ICollection<EventSector> EventSectors { get; set; } = new List<EventSector>();
}
