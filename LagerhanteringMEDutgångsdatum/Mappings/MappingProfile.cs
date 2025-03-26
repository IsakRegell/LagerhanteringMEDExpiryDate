using AutoMapper;
using LagerhanteringMEDutgångsdatum.DTOs;
using LagerhanteringMEDutgångsdatum.Models;

namespace LagerhanteringMEDutgångsdatum.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
