namespace Application.Models.Responses;

public class EventFullSnapshotResponse
{
    public Guid EventId { get; set; }
    public Guid VenueId { get; set; }
    public string Name { get; set; }
    public string BannerImageUrl { get; set; }
    public string ThumbnailUrl { get; set; }
    public string ThemeColor { get; set; }
    public string VenueBackgroundImageUrl { get; set; }
    public string MapUrl { get; set; }
    public List<EventSectorFullResponse> Sectors { get; set; } = new();
}