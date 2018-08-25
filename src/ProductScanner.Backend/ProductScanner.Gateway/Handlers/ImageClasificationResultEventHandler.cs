using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces.Events;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ImageClasificationResultEventHandler : IIntegrationEventHandler<ImageClasificationResultIntegrationEvent>
    {

        public ImageClasificationResultEventHandler()
        {
            System.Diagnostics.Trace.WriteLine("ctor");
        }

        public async Task Handle(ImageClasificationResultIntegrationEvent @event)
        {
            //logic here
            await Task.CompletedTask;
        }
    }
}
