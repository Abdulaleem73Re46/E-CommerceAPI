using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository;


public class RepositoryManager : IRepositoryManager
{


   private readonly RepositoryContext _repository;
   private readonly Lazy<ICartRepository> _cart;

private readonly Lazy<IOrderRepository> _order;
   private readonly Lazy<IProductRepository> _product;
      private readonly Lazy<IPaymentRepository> _payment;
         private readonly Lazy<ICategoryRepository> _category;
         
           private readonly Lazy<IIdempotencyRecordRepository> _idempotency;
           private readonly Lazy<IWebHookEventRepository> _webHookEventRepository;
           private readonly Lazy<IPaymentTransactionRepository> _paymentTransaction;


 public RepositoryManager(RepositoryContext repository)
    {
        _repository=repository;
         _cart=new Lazy<ICartRepository>(()=>new CartRepository(repository));
         _order=new Lazy<IOrderRepository>(()=>new OrderRepository(repository));
         _product=new Lazy<IProductRepository>(()=>new ProductRepository(repository));
         _category=new Lazy<ICategoryRepository>(()=>new CategoryRepository(repository));
      
        _payment=new Lazy<IPaymentRepository>(()=>new PaymentRepository(repository));
          _paymentTransaction = new Lazy<IPaymentTransactionRepository>(() => new PaymentTransactionRepository(repository));
        _webHookEventRepository = new Lazy<IWebHookEventRepository>(() => new WebhookEventRepository(repository));
        _idempotency = new Lazy<IIdempotencyRecordRepository>(() => new IdempotencyRecordRepository(repository));
        

    }

    public ICartRepository CartRepository =>_cart.Value;

    public IPaymentRepository PaymentRepository =>_payment.Value;



    public IProductRepository ProductRepository => _product.Value ;

    public IOrderRepository OrderRepository => _order.Value ;

    public ICategoryRepository CategoryRepository => _category.Value ;
    
  public IPaymentTransactionRepository PaymentTransactionRepository => _paymentTransaction.Value;
  //  public IWebHookEventRepository WebhookEventRepository => _webHookEventRepository.Value;
    public IIdempotencyRecordRepository IdempotencyRecordRepository => _idempotency.Value;

    public IWebHookEventRepository WebHookEventRepository => _webHookEventRepository.Value;

    public async Task SaveAsync()=>await _repository.SaveChangesAsync();

public async Task<IDbContextTransaction> BeginTransactionAsync()=>await _repository.Database.BeginTransactionAsync();


}


