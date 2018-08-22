using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Interfaces.Events
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
