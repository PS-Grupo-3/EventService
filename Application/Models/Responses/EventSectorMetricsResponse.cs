namespace Application.Models.Responses
{
    public class EventSectorMetricsResponse
    {
        public Guid SectorId { get; set; }
        public string Name { get; set; }
        public int TotalSeats { get; set; }
        public int SoldSeats { get; set; }
        public int AvailableSeats { get; set; }
        public double OccupancyRate { get; set; }
        public decimal Renueve { get; set; }
    }
}
