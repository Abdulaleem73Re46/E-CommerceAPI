using Core.Entities;


namespace Core.Contracts;


public interface IOrderRepository{
  
  
Task<Order?> GetByIdAsync(Guid OrderId,bool trackChanges);

  Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId,bool trackChanges);
  Task<Order?> GetOrderWithItemsAsync(Guid Id);
Task<IEnumerable<Order>> GetAllAsync();
Task<IEnumerable<Order>> GetByStatusAsync(string Orderstatus);
 // Task   AddAsync(Order Order);
    void  UpdateOrder(Order Order);
    void DeleteOrder(Order Order);
//Task<bool> ExistsAsync(Guid id);

}
