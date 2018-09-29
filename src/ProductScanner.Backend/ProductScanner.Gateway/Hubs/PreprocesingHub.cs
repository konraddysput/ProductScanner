using Microsoft.AspNetCore.SignalR;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces;
using ProductScanner.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Hubs
{
    public class PreprocesingHub : Hub
    {
        private readonly IPhotoObjectService _photoObjectService;
        private readonly IEventBus _eventBus;

        public PreprocesingHub(
            IPhotoObjectService photoObjectService,
            IEventBus eventBus)
        {
            _eventBus = eventBus;
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

        public async Task RefreshReports()
        {
            var report = new ReportEvent();
            _eventBus.Publish(report);
        }
    }
}
