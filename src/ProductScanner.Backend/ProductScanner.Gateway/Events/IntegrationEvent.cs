using System;

namespace ProductScanner.Gateway.Events
{
    public class IntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}
