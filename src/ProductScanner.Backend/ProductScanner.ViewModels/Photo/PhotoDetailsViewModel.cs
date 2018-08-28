using ProductScanner.ViewModels.PhotoObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.ViewModels.Photo
{
    public class PhotoDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime UploadDate { get; set; }
        public IEnumerable<PhotoObjectViewModel> PhotoObjects { get; set; }
    }
}
