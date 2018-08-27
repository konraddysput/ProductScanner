using ProductScanner.ViewModels.PhotoObject;
using System;
using System.Collections.Generic;

namespace ProductScanner.ViewModels.Photo
{
    public class PhotoViewModel : ViewModelBase
    {
        public string Path { get; set; }
        public string AnalysedFilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }

        public IEnumerable<PhotoObjectViewModel> PhotoObjects { get; set; }
    }
}

