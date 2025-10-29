namespace Application.Models.Requests;
public class CreateEventRequest
{
    public Guid VenueId { get; set; }
    public string UserToken { get; set; } = null!;
    public int CategoryId { get; set; }
    public int TypeId { get; set; }
    public int StatusId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Time { get; set; }
    public string Address { get; set; } = null!;
    public string BannerImageUrl { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public string ThemeColor { get; set; } = "#FFFFFF";
}