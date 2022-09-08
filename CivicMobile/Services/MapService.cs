//using CivicMobile.Interfaces;
using CivicMobile.Interfaces;
using System;
using System.Threading.Tasks;
//using Xamarin.Essentials;
namespace CivicMobile.Services
{
    public class MapService : IMapService
    {
        public async Task LoadMap()
        {
            await Map.OpenAsync(39.599146, -104.843006);
        }
    }
}
