

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

    public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsByOrderId(Guid orderId, bool trackChanges)
    {
            var order=await _repository.OrderRepository.GetOrderWithItemsAsync(orderId);
            var orderitems=_mapper.Map<IEnumerable<OrderItemDto>>(order);
            return orderitems;
    }

    public Task<IEnumerable<OrderDto>> GetAllOrdersAsync(bool trackChanges)
    {
         throw new NotImplementedException();
    }

    public Task<OrderDto?> GetOrderByIdAsync(Guid OrderId, bool trackChanges)
    {
    //    var order= _repository.OrderRepository.GetByIdAsync(OrderId,trackChanges);
    //    var orderdto=_mapper.Map<OrderDto>(order);
    //    return orderdto;
    throw new NotImplementedException();

    }

    public Task<IEnumerable<OrderDto>> GetOrdersByUserId(Guid userId, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId, bool trackChanges)
    {
        // var order=_repository.OrderRepository.GetOrderWithItemsAsync(orderId);
        // var payment=_repository.PaymentRepository.GetPaymentAsync(orderId,trackChanges);
        // throw new NotImplementedException();



        throw new NotImplementedException();
    }
}