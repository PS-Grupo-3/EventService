namespace Domain.Entities
{
    public class Event
    {
        public Guid EventId { get; set; }
        public Guid VenueId { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Description { get; set; } = null!;
        public int Status { get; set; }
        public DateTime Time { get; set; }
        public string Address { get; set; } = null!;
        public string MapUrl { get; set; } = null!;
        public string BannerImageUrl { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;
        public string ThemeColor { get; set; } = null!;

        public EventStatus EventStatus { get; set; } = null!;
        public EventCategory EventCategory { get; set; } = null!;
    }
}
