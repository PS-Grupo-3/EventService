using System.Net.Http.Json;
using Application.Models.Responses;
using Application.Interfaces.Adapter;

namespace Infrastructure.Adapter;

public class VenueHttpClient : IVenueClient
{
    private readonly HttpClient _http;

    public VenueHttpClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<VenueSnapshotDto?> GetVenue(string venueId)
    {
        return await _http.GetFromJsonAsync<VenueSnapshotDto>($"/api/v1/venues/{venueId}");
    }

    public async Task<SectorSnapshotDto?> GetSector(string sectorId)
    {
        return await _http.GetFromJsonAsync<SectorSnapshotDto>($"/api/v1/sector/{sectorId}");
    }
}
