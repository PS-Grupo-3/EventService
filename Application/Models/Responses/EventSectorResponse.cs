namespace Application.Models.Responses;

public class EventSectorResponse
{
    public Guid EventSectorId { get; set; }
    public Guid EventId { get; set; }
    public Guid SectorId { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
}
