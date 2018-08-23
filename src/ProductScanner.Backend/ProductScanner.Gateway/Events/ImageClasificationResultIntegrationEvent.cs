namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationResultIntegrationEvent : IntegrationEvent
    {
        public string Path { get; set; }
    }
}
