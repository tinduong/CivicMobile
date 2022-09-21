using CivicMobile.Interfaces;

namespace CivicMobile.Services;

public class MapService : IMapService
{
    public async Task LoadMap()
    {
        await Map.OpenAsync(39.599146, -104.843006);
    }
}