using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.ViewModels.PhotoObject
{
    public class PhotoObjectViewModel : ViewModelBase
    {
        public double Score { get; set; }
        public string Category { get; set; }

        //box positions
        public double PositionXL { get; set; }
        public double PositionYL { get; set; }
        public double PositionXR { get; set; }
        public double PositionYR { get; set; }

        public int PhotoId { get; set; }
    }
}
