namespace Application.Models.Requests;

public class UpdateEventSectorRequest
{
    public Guid EventSectorId { get; set; }
    public int? Capacity { get; set; }
    public decimal? Price { get; set; }
    public bool? Available { get; set; }
}
