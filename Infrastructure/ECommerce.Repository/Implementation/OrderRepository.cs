using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext repository) : base(repository)
    {
    }

   // public async Task AddAsync(Order Order)=>await Create(order);

    public void DeleteOrder(Order Order)=>Delete(Order);


   

    public async Task<IEnumerable<Order>> GetAllAsync()=>await FindAll(false)
    .OrderBy(o=>o.OrderDate)
    .ToListAsync();


    public async Task<Order?> GetByIdAsync(Guid OrderId, bool trackChanges)=>await FindByCondition(o=>o.OrderId.Equals(OrderId),trackChanges)
    .SingleOrDefaultAsync();

    public async Task<IEnumerable<Order>> GetByStatusAsync(string Orderstatus)=>await FindByCondition(o=>o.Status.Equals(Orderstatus),false)
    .OrderBy(o=>o.OrderDate)
    .ToListAsync();
    public async Task<Order?> GetOrderWithItemsAsync(Guid Id)=>await FindByCondition(o=>o.OrderId.Equals(Id),false).Include(oi=>oi.OrderItems)
    .SingleOrDefaultAsync();

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId, bool trackChanges)=>await FindByCondition(o=>o.UserId.Equals(userId),trackChanges)
    .OrderBy(o=>o.OrderDate)
    .ToListAsync();

    public void UpdateOrder(Order Order)=>Update(Order);

    // public void CreateOrder(Order Order)
    // {
    //     throw new NotImplementedException();
    // }

    // public void DeleteOrder(Order Order)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Order> GetOrderAsync(Guid OrderId, bool trackChanges)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<IEnumerable<Order>> GetOrdersAsync(bool trackChanges)
    // {
    //     throw new NotImplementedException();
    // }

    // public void UpdateOrder(Order Order)
    // {
    //     throw new NotImplementedException();
    // }
}