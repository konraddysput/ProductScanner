using System.Collections.Generic;

namespace ProductScanner.Gateway.Events
{
    public class ReportResultEvent : IntegrationEvent
    {
        public int TotalNumberOfIndividuals { get; set; }
        public int TotalNumberOfInvalidIndividuals { get; set; }
        public Dictionary<string,int> DetectedProducts { get; set; }
    }
}
