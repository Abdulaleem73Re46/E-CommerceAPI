

using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Enum.OrderStatus;
using Core.Enum.PaymentStatus;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service;



public sealed class OrderService : IOrderService
{

   private readonly IRepositoryManager _repository;
   private readonly IMapper _mapper;
private readonly IPaymentGateway _payment;
    public OrderService(IRepositoryManager repository,IPaymentGateway paymentGateway,IMapper mapper)
    {
        
        _repository=repository;
        _mapper=mapper;
        _payment=paymentGateway;
        
    }

    public async Task<OrderDto> CreateOrderAfterPaymentAsync(string userId,Guid cartId,ProcessPaymentForCreation processPaymentDto)
    {
        var cart=await _repository.CartRepository.GetCartWithItemsAsync(cartId);
        if(cart==null || !cart.CartItems.Any())
        {
            throw new InvalidOperationException("cart is empty");

        }

decimal total=cart.CartItems.Sum(i=>i.Quantity*i.UnitPrice);

var paymentResult=await _payment.ChargeAsync(total,processPaymentDto.paymentMethod);
if(!paymentResult.Succeeded) throw new Exception();
var payment=new Payment
{
    PaymentId=Guid.NewGuid(),
    Amount=total,
    
    PayMethod=processPaymentDto.paymentMethod,
    PayStatus=PaymentStatus.Success,
    PayDate=DateTime.UtcNow};



await _repository.PaymentRepository.AddAsync(payment);
await _repository.SaveAsync();
var order=new Order
{
  OrderId=Guid.NewGuid(),
  UserId=userId,
  TotalPrice=total,
  OrderDate=DateTime.UtcNow,
  Status=OrderStatus.Confirmed,
  PaymentId=payment.PaymentId,
  OrderItems=cart.CartItems.Select(item=> new OrderItem
  {
      OrderItemId=Guid.NewGuid(),
      ProductId=item.ProductId,
      Quantity=item.Quantity,
      PriceAtPurchase=item.UnitPrice
  }).ToList() 

};

_repository.OrderRepository.CreateOrder(userId,order);

foreach(var pro in cart.CartItems)
        {
            var product=await _repository.ProductRepository.GetProductAsync(pro.ProductId,trackChanges:false);
            product.StockQuantity-=pro.Quantity;
            _repository.ProductRepository.UpdateProduct(product);

        }
        
         await ClearCartItem(cartId);

       await   _repository.SaveAsync();


       return _mapper.Map<OrderDto>(order);
    }


private async Task ClearCartItem(Guid cartId)
    {
        
      var cartItems=await _repository.CartRepository.GetCartItemsByCartIdAsync(cartId);

    foreach (var item in cartItems)
        {
            
      _repository.CartRepository.RemoveItems(item);

    }
//await _repository.SaveAsync();


    }


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
     
    orderEntity.TotalPrice=orderEntity.OrderItems.Sum(i=>i.Quantity*i.PriceAtPurchase);
     
    
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
        // orderEntity.TotalPrice+=orderItem.PriceAtPurchase*orderItem.Quantity;

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

        return paymentDto;}

public async Task<PaymentDto> GetPaymentDtoAsync(Guid orderId)
    {
        var order=await _repository.OrderRepository.GetByIdAsync(orderId,false);
       var payment=await _repository.PaymentRepository.GetPaymentAsync(order.PaymentId,trackChanges:false);
       return _mapper.Map<PaymentDto>(payment);
       




    }

        
}