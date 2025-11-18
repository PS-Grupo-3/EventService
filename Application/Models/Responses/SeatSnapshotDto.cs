namespace Application.Models.Responses;

public class SeatSnapshotDto
{
    public long SeatId { get; set; }
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
}

