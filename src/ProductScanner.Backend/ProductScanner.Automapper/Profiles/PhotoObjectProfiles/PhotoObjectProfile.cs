using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.Gateway.Events;
using ProductScanner.ViewModels.PhotoObject;
using System;

namespace ProductScanner.Automapper.Profiles.PhotoObjectProfiles
{
    public class PhotoObjectProfile : Profile
    {
        public PhotoObjectProfile()
        {
            CreateMap<PhotoObject, PhotoObject>();
            CreateMap<PhotoObjectViewModel, PhotoObject>();
            CreateMap<PhotoObject, PhotoObjectViewModel>()
                .ForMember(n => n.PositionYMin, m => m.MapFrom(g => Math.Round(g.PositionYMin, 2)))
                .ForMember(n => n.PositionXMin, m => m.MapFrom(g => Math.Round(g.PositionXMin, 2)))
                .ForMember(n => n.PositionYMax, m => m.MapFrom(g => Math.Round(g.PositionYMax, 2)))
                .ForMember(n => n.PositionXMax, m => m.MapFrom(g => Math.Round(g.PositionXMax, 2)));

            CreateMap<PhotoObjectViewModel, ImageClasificationEventResultEntry>()
                .ForMember(n => n.Position, m => m.MapFrom(g => new double[] { g.PositionYMin, g.PositionXMin, g.PositionYMax, g.PositionXMax }))
                .ForMember(n => n.Id, m => m.MapFrom(g => g.Id));
            CreateMap<ImageClasificationEventResultEntry, PhotoObjectViewModel>()
                .ForMember(n => n.PositionYMin, m => m.MapFrom(g => g.Position[0]))
                .ForMember(n => n.PositionXMin, m => m.MapFrom(g => g.Position[1]))
                .ForMember(n => n.PositionYMax, m => m.MapFrom(g => g.Position[2]))
                .ForMember(n => n.PositionXMax, m => m.MapFrom(g => g.Position[3]));
        }
    }
}
