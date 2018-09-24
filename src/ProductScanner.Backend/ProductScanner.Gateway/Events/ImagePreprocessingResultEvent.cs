using System.Collections.Generic;

namespace ProductScanner.Gateway.Events
{
    public class ImagePreprocessingResultEvent : IntegrationEvent
    {
        public List<ImagePreprocessingResultEventEntry> Data { get; set; }
    }
}
