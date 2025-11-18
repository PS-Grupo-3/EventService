namespace Application.Models.Responses;

public class VenueSnapshotDto
{
    public Guid VenueId { get; set; }
    public string Name { get; set; }
    public List<SectorSnapshotDto> Sectors { get; set; }
}