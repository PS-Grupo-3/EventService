namespace Application.Models.Responses;

public class EventResponse
{
    public Guid EventId { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string CategoryType { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime Time { get; set; }
    public string Address { get; set; } = null!;
    public string BannerImageUrl { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
}
