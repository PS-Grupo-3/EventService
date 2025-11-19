namespace Application.Models.Responses;

public class EventSeatResponse
{
    public Guid EventSeatId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
}