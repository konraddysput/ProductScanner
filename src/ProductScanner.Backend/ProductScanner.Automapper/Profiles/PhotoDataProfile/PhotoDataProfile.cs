using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.ViewModels.PhotoData;
using System.Collections.Generic;

namespace ProductScanner.Automapper.Profiles.PhotoDataProfile
{
    public class PhotoDataProfile : Profile
    {
        public PhotoDataProfile()
        {
            CreateMap<PhotoData, PhotoData>();
            CreateMap<PhotoDataViewModel, PhotoDataViewModel>();
            CreateMap<PhotoDataViewModel, PhotoData>()
                .ForMember(n => n.Id, m => m.Ignore());
            CreateMap<PhotoObject, PhotoDataViewModel>();
            CreateMap<KeyValuePair<string, string>, PhotoDataViewModel>()
                .ForMember(n => n.Type, m => m.MapFrom(g => g.Key))
                .ForMember(n => n.Value, m => m.MapFrom(g => g.Value));
        }
    }
}
