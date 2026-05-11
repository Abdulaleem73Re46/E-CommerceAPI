using System.ComponentModel;
using AutoMapper;
using Core.Entities;
using Core.Shared.DataTransferObjects;





public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Category,CategoryDto>();


        CreateMap<Product,ProductDto>();
        CreateMap<Order,OrderDto>();


    }






}