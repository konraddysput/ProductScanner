using System.Collections.Generic;

namespace ProductScanner.Gateway.Events
{
    public class ImagePreprocessingEvent : IntegrationEvent
    {
        public IEnumerable<ImageClasificationEventResultEntry> Entries { get; set; }
    }
}
