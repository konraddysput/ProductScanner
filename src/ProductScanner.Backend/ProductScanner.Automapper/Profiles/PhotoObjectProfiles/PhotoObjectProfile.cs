using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.Gateway.Events;
using ProductScanner.ViewModels.PhotoObject;
using static ProductScanner.Gateway.Events.ImageClasificationResultEvent;

namespace ProductScanner.Automapper.Profiles.PhotoObjectProfiles
{
    public class PhotoObjectProfile : Profile
    {
        public PhotoObjectProfile()
        {
            CreateMap<PhotoObject, PhotoObject>();
            CreateMap<PhotoObjectViewModel, PhotoObject>();
            CreateMap<PhotoObject, PhotoObjectViewModel>();
            CreateMap<ImageClasificationEventResultEntry, PhotoObjectViewModel>()
                .ForMember(n => n.PositionXL, m => m.MapFrom(g => g.Position[0])) 
                .ForMember(n => n.PositionYL, m => m.MapFrom(g => g.Position[1]))
                .ForMember(n => n.PositionXR, m => m.MapFrom(g => g.Position[2]))
                .ForMember(n => n.PositionYR, m => m.MapFrom(g => g.Position[3]));
        }
    }
}
