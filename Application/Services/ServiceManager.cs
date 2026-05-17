using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

    namespace Service;
    






    public sealed class ServiceManager : IServiceManager
    {

        
    private readonly Lazy<ICartService> _cartService;
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IOrderService> _orderService;

    private readonly Lazy<IPaymentService> _paymentService;

private readonly Lazy<IAuthenticationService> _Authentication;


    public ServiceManager(IRepositoryManager repository,IMapper mapper,UserManager<User>  user,IConfiguration configuration)
    {

    _categoryService=new Lazy<ICategoryService>(()=>new CategoryService(repository,mapper));
    _productService=new Lazy<IProductService>(()=>new ProductService(repository,mapper));
    _cartService=new Lazy<ICartService>(()=>new CartService(repository,mapper));
    _orderService=new Lazy<IOrderService>(()=>new OrderService(repository,mapper));
    _paymentService=new Lazy<IPaymentService>(()=>new PaymentService(repository,mapper));
    _Authentication=new Lazy<IAuthenticationService>(()=>new AuthenticationService(mapper,user,configuration));
    
        
    }







        public ICartService CartService =>_cartService.Value;


        public ICategoryService CategoryService =>_categoryService.Value;


        public IProductService ProductService =>_productService.Value;

        public IPaymentService PaymentService =>_paymentService.Value;

        public IOrderService OrderService =>_orderService.Value;

    public IAuthenticationService AuthenticationService => _Authentication.Value;
}