namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationIntegrationEvent : IntegrationEvent
    {
        public int PhotoId { get; set; }
        public string Path { get; set; }
    }
}
