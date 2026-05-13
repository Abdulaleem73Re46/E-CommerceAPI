

using AutoMapper;
using Core.Contracts;
using Core.Entities;
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

    public async Task<OrderForCreationDto> CreateOrderAsync(string userId,OrderDto order, bool trackChanges)
    { 
          var Orderentity=_mapper.Map<Order>(order);
          _repository.OrderRepository.CreateOrder(userId,Orderentity);
         await  _repository.SaveAsync();
          var entityToReturn=_mapper.Map<OrderForCreationDto>(Orderentity);
          return entityToReturn;


         
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

    public async Task<OrderDto?> GetOrderByIdAsync(Guid OrderId, bool trackChanges)
    {
       var order=await _repository.OrderRepository.GetByIdAsync(OrderId,trackChanges);
       var orderdto=_mapper.Map<OrderDto>(order);
       return orderdto;


    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByUserId(Guid userId, bool trackChanges)
    {
          var order=await _repository.OrderRepository.GetUserOrdersAsync(userId,trackChanges);
          var orderDtos=_mapper.Map<IEnumerable<OrderDto>>(order);
          return orderDtos;
    }

    public async Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId, bool trackChanges)
    {
        
        var payment=await _repository.PaymentRepository.GetPaymentAsync(orderId,trackChanges);

        var paymentDto=_mapper.Map<PaymentDto>(payment);

        return paymentDto;
      



        
    }
}