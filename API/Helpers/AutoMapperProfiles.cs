using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{ 
    public AutoMapperProfiles() // any properties that match the name will get mapped, even if cases are different, it also ginores 'Get' in the GetAge instance
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url)); 
        CreateMap<Photo, PhotoDto>();  
    }
}