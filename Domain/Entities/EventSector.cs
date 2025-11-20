namespace Domain.Entities;

public class EventSector
{
    public Guid EventSectorId { get; set; }
    public Guid EventId { get; set; }
    public Guid VenueSectorId { get; set; }  

    public string Name { get; set; } = null!;
    public bool IsControlled { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }

    public EventSectorShape Shape { get; set; } = null!;
    public ICollection<EventSeat> Seats { get; set; } = new List<EventSeat>();

    public Event Event { get; set; } = null!;
}

