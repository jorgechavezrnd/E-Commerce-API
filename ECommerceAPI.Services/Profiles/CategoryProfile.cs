using AutoMapper;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Services.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(o => o.Id, d => d.MapFrom(x => x.Id))
                .ForMember(o => o.Name, d => d.MapFrom(x => x.Name))
                .ForMember(o => o.Description, d => d.MapFrom(x => x.Description))
                .ReverseMap();

            CreateMap<Category, CategoryRequest>()
                .ForMember(o => o.Name, d => d.MapFrom(x => x.Name))
                .ForMember(o => o.Description, d => d.MapFrom(x => x.Description))
                .ReverseMap();
        }
    }
}
