using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.ViewModels.PhotoType;

namespace ProductScanner.Automapper.Profiles.PhotoTypeProfile
{
    public class PhotoTypeProfile : Profile
    {
        public PhotoTypeProfile()
        {
            CreateMap<PhotoType, PhotoType>();
            CreateMap<PhotoTypeViewModel, PhotoTypeViewModel>();
            CreateMap<PhotoTypeViewModel, PhotoType>()
                .ForMember(n => n.Id, m => m.Ignore());
            CreateMap<PhotoType, PhotoTypeViewModel>();
        }
    }
}
