using AutoMapper;
using Template.Net.Microservice.DDD.Domain.Entity;

namespace Template.Net.Microservice.DDD.Application.Dtos.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<ProductDto, Product>()
            .ReverseMap();
    }
}