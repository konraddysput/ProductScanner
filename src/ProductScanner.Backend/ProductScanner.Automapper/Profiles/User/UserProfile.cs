using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.ViewModels.Token;

namespace ProductScanner.Automapper.Profiles.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>(MemberList.None)
                .ForMember(n => n.UserName, d => d.MapFrom(p => p.Login));
        }
    }
}
