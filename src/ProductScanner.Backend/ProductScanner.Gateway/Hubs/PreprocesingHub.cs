using Microsoft.AspNetCore.SignalR;
using ProductScanner.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Hubs
{
    public class PreprocesingHub : Hub
    {
        private readonly IPhotoObjectService _photoObjectService;

        public PreprocesingHub(IPhotoObjectService photoObjectService)
        {
            _photoObjectService = photoObjectService;
        }
        public async Task DataReady(int id)
        {
            await Clients.All.SendAsync("DataReady", id, true);
        }

        public async Task IsDataReady(int id)
        {
            var any = await _photoObjectService.Any(n => n.PhotoId == id);
            await Clients.Caller.SendAsync("DataReady", id, any);
        }
    }
}
