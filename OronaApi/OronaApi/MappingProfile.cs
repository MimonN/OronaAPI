using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace OronaApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<WindowType, WindowTypeDto>().ReverseMap();
            CreateMap<WindowType, WindowTypeCreateDto>().ReverseMap();
            CreateMap<WindowType, WindowTypeUpdateDto>().ReverseMap();
        }
    }
}
