using Core.Contracts;
using Core.Entities;

namespace Repository;


public class RepositoryManager : IRepositoryManager
{


   private readonly RepositoryContext _repository;
   private readonly Lazy<ICartRepository> _cart;

private readonly Lazy<IOrderRepository> _order;
   private readonly Lazy<IProductRepository> _product;
      private readonly Lazy<IPaymentRepository> _payment;
         private readonly Lazy<ICategoryRepository> _category;
         
           

 public RepositoryManager(RepositoryContext repository)
    {
        _repository=repository;
         _cart=new Lazy<ICartRepository>(()=>new CartRepository(repository));
         _order=new Lazy<IOrderRepository>(()=>new OrderRepository(repository));
         _product=new Lazy<IProductRepository>(()=>new ProductRepository(repository));
         _category=new Lazy<ICategoryRepository>(()=>new CategoryRepository(repository));
      
        _payment=new Lazy<IPaymentRepository>(()=>new PaymentRepository(repository));
         

    }

    public ICartRepository CartRepository =>_cart.Value;

    public IPaymentRepository PaymentRepository =>_payment.Value;



    public IProductRepository ProductRepository => _product.Value ;

    public IOrderRepository OrderRepository => _order.Value ;

    public ICategoryRepository CategoryRepository => _category.Value ;
    
    public async Task SaveAsync()=>await _repository.SaveChangesAsync();
}


