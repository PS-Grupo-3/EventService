namespace Application.Models.Requests;

public class UpdateEventRequest
{
    public Guid EventId { get; set; }
    public int? CategoryId { get; set; }
    public int? TypeId { get; set; }
    public int? StatusId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Time { get; set; }
    public string? Address { get; set; }
    public string? BannerImageUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? ThemeColor { get; set; }
}
