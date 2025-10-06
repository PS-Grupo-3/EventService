namespace Domain.Entities
{
    public class Event
    {
        public Guid EventId { get; set; }
        public Guid VenueId { get; set; }
        public Guid Administrator {  get; set; }

        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Description { get; set; } = null!;

        public int Status { get; set; }
        public EventStatus EventStatus { get; set; } = null!;
    }
}
