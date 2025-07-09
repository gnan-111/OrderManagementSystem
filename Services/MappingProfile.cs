// MappingProfile.cs
using AutoMapper;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<OrderCreateDTO, Order>().ReverseMap();
    }
}
