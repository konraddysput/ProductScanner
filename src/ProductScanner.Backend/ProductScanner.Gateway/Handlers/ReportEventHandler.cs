using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Hubs;
using ProductScanner.Gateway.Interfaces.Events;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ReportEventHandler : IIntegrationEventHandler<ReportResultEvent>
    {
        private readonly IHubContext<PreprocesingHub> _hub;
        public ReportEventHandler(IHubContext<PreprocesingHub> hub)
        {
            _hub = hub;
        }

        public async Task Handle(ReportResultEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event);
            await _hub.Clients.All.SendAsync("ReportReady", json);
        }
    }
}
