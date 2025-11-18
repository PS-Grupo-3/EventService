using Application.Models.Responses;
namespace Application.Interfaces.Adapter;

public interface IVenueClient
{
    Task<VenueSnapshotDto?> GetVenue(string venueId);
    Task<SectorSnapshotDto?> GetSector(string sectorId);
}