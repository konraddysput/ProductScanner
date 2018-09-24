using ProductScanner.ViewModels.PhotoData;
using ProductScanner.ViewModels.PhotoType;
using System.Collections.Generic;

namespace ProductScanner.ViewModels.PhotoObject
{
    public class PhotoObjectViewModel : ViewModelBase
    {
        public double Score { get; set; }
        public string Category { get; set; }

        //box positions
        public double PositionYMin { get; set; }
        public double PositionXMin { get; set; }
        public double PositionYMax { get; set; }
        public double PositionXMax { get; set; }

        public int PhotoId { get; set; }

        public IEnumerable<PhotoTypeViewModel> PhotoTypes { get; set; }
        public IEnumerable<PhotoDataViewModel> PhotoData { get; set; }
    }
}
