using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.Gateway.Events
{
    public class ImageClasificationEventResultEntry
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public double[] Position { get; set; }
        public string Category { get; set; }
    }
}
