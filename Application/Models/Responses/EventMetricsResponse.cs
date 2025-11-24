namespace Application.Models.Responses
{
    public class EventMetricsResponse
    {
        public Guid EventId { get; set; }
        public Guid VenueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        public string Address { get; set; }
        public string BannerImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ThemeColor { get; set; }
        public string VenueBackgroundImageUrl { get; set; }
        public string MapUrl { get; set; }
        public int TotalSeats { get; set; }
        public int SoldSeats { get; set; }
        public int AvailableSeats { get; set; }
        public double OcupancyRate { get; set; }
        public decimal TotalRenueve { get; set; }
        public List<EventSectorMetricsResponse> Sectors {  get; set; }
    }
}

