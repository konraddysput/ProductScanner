﻿using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.Gateway.Events;
using ProductScanner.ViewModels.Photo;
using ProductScanner.ViewModels.PhotoObject;
using System.Collections;
using System.Collections.Generic;

namespace ProductScanner.Automapper.Profiles.PhotoProfiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, Photo>();
            CreateMap<Photo, ImageClasificationIntegrationEvent>();
            CreateMap<PhotoViewModel, Photo>();
            CreateMap<Photo, PhotoViewModel>();
            CreateMap<ImageClasificationResultIntegrationEvent, PhotoViewModel>()
                .ForMember(m => m.UserId, p => p.Ignore())
                .ForMember(m => m.PhotoObjects,
                    p => p.MapFrom(s => s.Result));
        }
    }
}
