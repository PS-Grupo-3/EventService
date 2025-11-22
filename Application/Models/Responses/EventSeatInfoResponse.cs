namespace Application.Models.Responses;

public class EventSeatInfoResponse
{
    public Guid EventSeatId { get; set; }
    public Guid EventSectorId { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public decimal Price { get; set; }
}
