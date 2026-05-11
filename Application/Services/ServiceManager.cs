using AutoMapper;
using Core.Contracts;
    using Service.Contracts;

    namespace Service;
    






    public sealed class ServiceManager : IServiceManager
    {

        
    private readonly Lazy<ICartService> _cartService;
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IOrderService> _orderService;

    private readonly Lazy<IPaymentService> _paymentService;



    public ServiceManager(IRepositoryManager repository,IMapper mapper)
    {

    _categoryService=new Lazy<ICategoryService>(()=>new CategoryService(repository,mapper));
    _productService=new Lazy<IProductService>(()=>new ProductService(repository,mapper));
    _cartService=new Lazy<ICartService>(()=>new CartService(repository,mapper));
    _orderService=new Lazy<IOrderService>(()=>new OrderService(repository,mapper));
    _paymentService=new Lazy<IPaymentService>(()=>new PaymentService(repository,mapper));
        
    }







        public ICartService CartService =>_cartService.Value;


        public ICategoryService CategoryService =>_categoryService.Value;


        public IProductService ProductService =>_productService.Value;

        public IPaymentService PaymentService =>_paymentService.Value;

        public IOrderService OrderService =>_orderService.Value;
    }