

using AutoMapper;
using Core.Contracts;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service;



public sealed class OrderService : IOrderService
{

   private readonly IRepositoryManager _repository;
   private readonly IMapper _mapper;

    public OrderService(IRepositoryManager repository,IMapper mapper)
    {
        
        _repository=repository;
        _mapper=mapper;
        
    }

    public Task<OrderForCreationDto> CreateOrderAsync(OrderDto order, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderItemDto>> GetAllOrderItemsByOrderId(Guid orderId, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDto>> GetAllOrdersAsync(bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetOrderByIdAsync(Guid OrderId, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDto>> GetOrdersByUserId(Guid userId, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}