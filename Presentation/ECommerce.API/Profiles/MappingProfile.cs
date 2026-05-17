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
        CreateMap<Product, ProductDto>().ReverseMap();
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
      //  CreateMap<OrderItemForCreationDto, OrderItem>();
        
        // Payment mappings
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<PaymentForCreationDto, Payment>();
        
        // User mappings
        // CreateMap<User, UserDto>().ForMember(dest=>dest.UserId,opt=>opt.MapFrom(src=>src.Id)).ReverseMap();
        // CreateMap<UserForRegisterDto,User>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>Guid.NewGuid().ToString()));

        // CreateMap<UserLoginDto,User>();
        // CreateMap<UserForUpdateDto,User>().ReverseMap();






        CreateMap<UserForRegisterDto, User>()
            .ForMember(dest => dest.UserName, 
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Id, 
                opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.SecurityStamp, 
                opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.ConcurrencyStamp, 
                opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.EmailConfirmed, 
                opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.PhoneNumberConfirmed, 
                opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.TwoFactorEnabled, 
                opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.LockoutEnabled, 
                opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.AccessFailedCount, 
                opt => opt.MapFrom(src => 0));
        
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber));



    }
}