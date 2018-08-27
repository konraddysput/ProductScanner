using System.Collections.Generic;

namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationResultIntegrationEvent : IntegrationEvent
    {
        public class ImageClassificationResultIntegrationEventEntry
        {
            public double Score { get; set; }
            public double[] Position { get; set; }
            public string Category { get; set; }
        }
        public string Path { get; set; }
        public string AnalysedFilePath { get; set; }

        public IEnumerable<ImageClassificationResultIntegrationEventEntry> Result { get; set; }
    }
}
