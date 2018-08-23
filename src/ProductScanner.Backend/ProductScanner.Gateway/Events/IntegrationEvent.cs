using System;

namespace ProductScanner.Gateway.Events
{
    public class IntegrationEvent
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}
