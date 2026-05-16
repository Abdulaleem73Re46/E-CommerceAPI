using Core.Entities;
using Core.Shared.DataTransferObjects;

namespace Service.Contracts;




public interface IOrderService
{
    Task<OrderDto?> GetOrderByIdAsync(Guid OrderId,bool trackChanges);


    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(bool trackChanges);
    Task<IEnumerable<OrderDto>> GetOrdersByUserId(string userId,bool trackChanges);
   
   Task<IEnumerable<OrderItemDto>> GetAllOrderItemsByOrderId(Guid orderId,bool trackChanges);

   Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId,bool trackChanges);


   Task<OrderDto> CreateOrderAsync(string userId,OrderForCreationDto order,bool trackChanges);

   
   Task<bool> DeleteOrderByIdAsync(Guid orderId);
   




   







    
}
