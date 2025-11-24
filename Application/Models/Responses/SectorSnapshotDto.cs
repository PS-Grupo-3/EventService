namespace Application.Models.Responses;

public class SectorSnapshotDto
{
    public Guid SectorId { get; set; }
    public string Name { get; set; }
    public bool IsControlled { get; set; }
    public int? Capacity { get; set; }
    public int? SeatCount { get; set; }
    public ShapeSnapshotDto? Shape { get; set; }
    public List<SeatSnapshotDto>? Seats { get; set; }
}