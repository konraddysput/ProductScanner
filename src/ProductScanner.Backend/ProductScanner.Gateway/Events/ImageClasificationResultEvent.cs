using System.Collections.Generic;

namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationResultEvent : IntegrationEvent
    {
        public string Path { get; set; }
        public string AnalysedFilePath { get; set; }

        public IEnumerable<ImageClasificationEventResultEntry> Data { get; set; }
    }
}
