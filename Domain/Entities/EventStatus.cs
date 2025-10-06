namespace Domain.Entities
{
    public class EventStatus
    {
        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; }
    }
}
