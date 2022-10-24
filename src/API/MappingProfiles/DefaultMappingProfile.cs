using API.Data.Dtos;
using API.Data.Entities;
using AutoMapper;

namespace API.MappingProfiles;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        CreateMap<Person, PersonDto>();
    }
}