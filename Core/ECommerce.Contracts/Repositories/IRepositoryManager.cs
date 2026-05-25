using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Contracts;



public interface IRepositoryManager
{
    ICartRepository CartRepository{get;}
    IPaymentRepository PaymentRepository{get;}
  
    IProductRepository ProductRepository{get;}
    IOrderRepository OrderRepository{get;}
    ICategoryRepository CategoryRepository{get;}


    Task  SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}