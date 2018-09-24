using AutoMapper;
using ProductScanner.Gateway.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.Automapper.Profiles.EventsProfile
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            CreateMap<ImageClasificationResultEvent, ImagePreprocessingEvent>();
            CreateMap<ImageClasificationEventResultEntry, ImageClasificationEventResultEntry>();
        }
    }
}
