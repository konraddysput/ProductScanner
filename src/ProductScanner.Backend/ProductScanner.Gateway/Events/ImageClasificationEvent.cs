using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationEvent : IntegrationEvent
    {
        public string Path { get; set; }
    }
}
