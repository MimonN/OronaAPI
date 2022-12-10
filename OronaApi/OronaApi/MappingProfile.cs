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
            CreateMap<WindowType, WindowTypeWithProductsDto>().ReverseMap();
            CreateMap<WindowType, WindowTypeCreateDto>().ReverseMap();
            CreateMap<WindowType, WindowTypeUpdateDto>().ReverseMap();

            CreateMap<CleaningType, CleaningTypeDto>().ReverseMap();
            CreateMap<CleaningType, CleaningTypeCreateDto>().ReverseMap();
            CreateMap<CleaningType, CleaningTypeUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
        }
    }
}
