

using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Enum.OrderStatus;
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

// public async Task<OrderForCreationDto> CreateOrderAsync(string userId, OrderForCreationDto order, bool trackChanges)
// {
//     var orderEntity = _mapper.Map<Order>(order);
//     orderEntity.UserId = userId;
//     orderEntity.OrderDate = DateTime.UtcNow;
//     orderEntity.Status = OrderStatus.Pending;
    
//     _repository.OrderRepository.CreateOrder(userId, orderEntity);
//     await _repository.SaveAsync();
    
//     var entityToReturn = _mapper.Map<OrderForCreationDto>(orderEntity);
//     return entityToReturn;



         
//     }

public async Task<OrderDto> CreateOrderAsync(string userId, OrderForCreationDto orderDto, bool trackChanges)
{
   
    var orderEntity = new Order
    {
        OrderId = Guid.NewGuid(),
        UserId = userId,
        TotalPrice =0.0m,
        OrderDate = DateTime.UtcNow,
        Status = OrderStatus.Pending,
        OrderItems = new List<OrderItem>()
    };
    
    
    foreach (var itemDto in orderDto.OrderItems)
    {
      
        var product = await _repository.ProductRepository.GetProductAsync(itemDto.ProductId, false);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} not found");
        
        var orderItem = new OrderItem
        {
            OrderItemId = Guid.NewGuid(),
            OrderId = orderEntity.OrderId,
            ProductId = itemDto.ProductId,
            Quantity = itemDto.Quantity,
            PriceAtPurchase = product.Price
        };
        orderEntity.TotalPrice+=orderItem.PriceAtPurchase*orderItem.Quantity;

        orderEntity.OrderItems.Add(orderItem);
        
        // Update product stock
        product.StockQuantity -= itemDto.Quantity;
        if (product.StockQuantity < 0)
            throw new InvalidOperationException($"Insufficient stock for product {product.Name}");
        
        _repository.ProductRepository.UpdateProduct(product);
    }
    
    // Save order
    _repository.OrderRepository.CreateOrder(userId, orderEntity);
    await _repository.SaveAsync();
    
    // Return the created order
    var entityToReturn = _mapper.Map<OrderDto>(orderEntity);
    return entityToReturn;
}

    public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
    {var order=await _repository.OrderRepository.GetByIdAsync(orderId,false);
    if(order is null)throw new KeyNotFoundException($"order with id {orderId} not found in database");

    _repository.OrderRepository.DeleteOrder(order);
    return true;
    
        
    }

    public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsByOrderId(Guid orderId, bool trackChanges)
    {
            var order=await _repository.OrderRepository.GetOrderWithItemsAsync(orderId);
            var orderitems=_mapper.Map<IEnumerable<OrderItemDto>>(order);
            return orderitems;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(bool trackChanges)
    {
     var orders=await _repository.OrderRepository.GetAllAsync();
     var orderDto=_mapper.Map<IEnumerable<OrderDto>>(orders);
     return orderDto;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid OrderId, bool trackChanges)
    {
       var order=await _repository.OrderRepository.GetByIdAsync(OrderId,trackChanges);
       var orderdto=_mapper.Map<OrderDto>(order);
       return orderdto;


    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByUserId(string userId, bool trackChanges)
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