using System.Collections.Generic;

namespace ProductScanner.Gateway.Events
{
    public class ImagePreprocessingResultEventEntry
    {
        public int Id { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public List<string> Types { get; set; }
    }
}
