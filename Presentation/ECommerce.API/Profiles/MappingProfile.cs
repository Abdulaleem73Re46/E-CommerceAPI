using AutoMapper;
using Core.Entities;
using Core.Shared.DataTransferObjects;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category mappings
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryForCreationDto, Category>();
        CreateMap<CategoryForUpdateDto, Category>();
        
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
            .ReverseMap();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        
        // Cart mappings
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap(); //            .ForMember(dest => dest.Name, 
               // opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
        
        // Order mappings
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderForCreationDto, Order>();
        CreateMap<OrderItemForCreationDto, OrderItem>();
        
        // Payment mappings
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<PaymentForCreationDto, Payment>();
        
        // User mappings
        CreateMap<User, UserDto>().ReverseMap();
    }
}