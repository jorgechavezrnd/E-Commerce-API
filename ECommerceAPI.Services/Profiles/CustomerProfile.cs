using AutoMapper;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Services.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(origen => origen.Id, destino => destino.MapFrom(x => x.Id))
                .ForMember(origen => origen.Name, destino => destino.MapFrom(x => x.Name))
                .ForMember(origen => origen.LastName, destino => destino.MapFrom(x => x.LastName))
                .ForMember(origen => origen.BirthDate, destino => destino.MapFrom(x => x.BirthDate.ToString("yyyy-MM-dd")))
                .ForMember(origen => origen.Dni, destino => destino.MapFrom(x => x.Dni))
                .ForMember(origen => origen.Email, destino => destino.MapFrom(x => x.Email))
                .ReverseMap();

            CreateMap<Customer, CustomerDtoRequest>()
                .ForMember(origen => origen.Name, destino => destino.MapFrom(x => x.Name))
                .ForMember(origen => origen.LastName, destino => destino.MapFrom(x => x.LastName))
                .ForMember(origen => origen.BirthDate, destino => destino.MapFrom(x => x.BirthDate.ToString("yyyy-MM-dd")))
                .ForMember(origen => origen.Dni, destino => destino.MapFrom(x => x.Dni))
                .ForMember(origen => origen.Email, destino => destino.MapFrom(x => x.Email))
                .ReverseMap();
        }
    }
}
