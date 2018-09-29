namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationEvent : IntegrationEvent
    {
        public string Path { get; set; }
    }
}
