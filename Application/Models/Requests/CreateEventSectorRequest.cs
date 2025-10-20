namespace Application.Models.Requests;

public class CreateEventSectorRequest
{
    public Guid EventId { get; set; }
    public Guid SectorId { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
}
