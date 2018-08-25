using AutoMapper;
using ProductScanner.Gateway.Events;

namespace ProductScanner.Automapper.Profiles.Photo
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Database.Entities.Photo, ImageClasificationIntegrationEvent>();
        }
    }
}
