namespace Application.Models.Responses;

public class EventSectorFullResponse
{
    public Guid EventSectorId { get; set; }
    public string Name { get; set; }
    public bool IsControlled { get; set; }
    public int Capacity { get; set; }
    public bool Available { get; set; }
    public decimal? Price { get; set; } 
    public EventSectorShapeResponse Shape { get; set; }
    public List<EventSeatResponse> Seats { get; set; } = new();
}