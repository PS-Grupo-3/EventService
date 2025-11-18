namespace Domain.Entities;

public class EventSectorShape
{
    public Guid EventSectorShapeId { get; set; }
    public Guid EventSectorId { get; set; }

    public string Type { get; set; } = null!;
    public int Width { get; set; }
    public int Height { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Rotation { get; set; }
    public int Padding { get; set; }
    public int Opacity { get; set; }
    public string Colour { get; set; } = null!;

    public EventSector EventSector { get; set; } = null!;
}
