using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>(); // any properties that match the name will get mapped, even if cases are different
        CreateMap<Photo, PhotoDto>(); // it also ginores 'Get' in the GetAge instance
    }
}