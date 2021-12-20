using AutoMapper;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;

namespace ECommerceAPI.Services.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductInfo, ProductDto>()
                .ForMember(origen => origen.Id, destino => destino.MapFrom(x => x.Id))
                .ForMember(origen => origen.ProductName, destino => destino.MapFrom(x => x.ProductName))
                .ForMember(origen => origen.ProductDescription, destino => destino.MapFrom(x => x.ProductDescription))
                .ForMember(origen => origen.Category, destino => destino.MapFrom(x => x.Category))
                .ForMember(origen => origen.UnitPrice, destino => destino.MapFrom(x => x.UnitPrice))
                .ForMember(origen => origen.UrlProduct, destino => destino.MapFrom(x => x.UrlProduct))
                .ForMember(origen => origen.Active, destino => destino.MapFrom(x => x.Active))
                .ReverseMap();

            CreateMap<Product, ProductSingleDto>()
                .ForMember(origen => origen.Id, destino => destino.MapFrom(x => x.Id))
                .ForMember(origen => origen.ProductName, destino => destino.MapFrom(x => x.Name))
                .ForMember(origen => origen.ProductDescription, destino => destino.MapFrom(x => x.Description))
                .ForMember(origen => origen.CategoryId, destino => destino.MapFrom(x => x.CategoryId))
                .ForMember(origen => origen.UnitPrice, destino => destino.MapFrom(x => x.UnitPrice))
                .ForMember(origen => origen.UrlProduct, destino => destino.MapFrom(x => x.ProductUrl))
                .ForMember(origen => origen.Active, destino => destino.MapFrom(x => x.Active))
                .ReverseMap();

            CreateMap<Product, ProductDtoRequest>()
                .ForMember(origen => origen.Name, destino => destino.MapFrom(x => x.Name))
                .ForMember(origen => origen.Description, destino => destino.MapFrom(x => x.Description))
                .ForMember(origen => origen.CategoryId, destino => destino.MapFrom(x => x.CategoryId))
                .ForMember(origen => origen.UnitPrice, destino => destino.MapFrom(x => x.UnitPrice))
                .ForMember(origen => origen.UrlImage, destino => destino.MapFrom(x => x.ProductUrl))
                .ForMember(origen => origen.Active, destino => destino.MapFrom(x => x.Active))
                .ReverseMap();
        }
    }
}
