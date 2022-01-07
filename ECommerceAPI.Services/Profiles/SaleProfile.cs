using AutoMapper;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;

namespace ECommerceAPI.Services.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<InvoiceInfo, SaleDtoSingleResponse>()
                .ForMember(origen => origen.SaleId, dest => dest.MapFrom(x => x.Id))
                .ForMember(origen => origen.Customer, dest => dest.MapFrom(x => x.CustomerName))
                .ForMember(origen => origen.SaleDate, dest => dest.MapFrom(x => x.SaleDate))
                .ForMember(origen => origen.InvoiceNumber, dest => dest.MapFrom(x => x.InvoiceNumber))
                .ForMember(origen => origen.SaleTotal, dest => dest.MapFrom(x => x.TotalAmount))
                .ReverseMap();

            CreateMap<SaleDetailDtoRequest, SaleDetail>()
                .ForMember(origen => origen.ProductId, dest => dest.MapFrom(x => x.ProductId))
                .ForMember(origen => origen.UnitPrice, dest => dest.MapFrom(x => x.UnitPrice))
                .ForMember(origen => origen.Quantity, dest => dest.MapFrom(x => x.Quantity))
                .ReverseMap();

            CreateMap<InvoiceDetailInfo, SaleDetailSingleResponse>()
                .ForMember(origen => origen.ItemId, dest => dest.MapFrom(x => x.ItemNumber))
                .ForMember(origen => origen.ProductName, dest => dest.MapFrom(x => x.ProductName))
                .ForMember(origen => origen.UnitPrice, dest => dest.MapFrom(x => x.UnitPrice))
                .ForMember(origen => origen.Quantity, dest => dest.MapFrom(x => x.Quantity))
                .ForMember(origen => origen.TotalPrice, dest => dest.MapFrom(x => x.Total))
                .ReverseMap();
        }
    }
}
