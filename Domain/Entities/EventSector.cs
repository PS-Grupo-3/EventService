namespace Domain.Entities
{
    public class EventSector
    {
        public Guid EventSectorId { get; set; }
        public Guid EventId { get; set; }
        public Guid SectorId { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }

        public Event Event { get; set; } = null!;
    }
}
