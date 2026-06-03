

using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Core.Contracts;
using Core.Entities;
using Core.Enum.OrderStatus;
using Core.Enum.PaymentStatus;
using Core.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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



// private async Task   CreateOrderFromCart(string userId)
//     {
//        // there is no userId but it's simulation ,the real is i think Id search 
//      var cart=await _repository.CartRepository.GetByUserIdAsync(userId);
// if (cart is  null)throw new Exception("djhdhdhd");
// var cartitems= await _repository.CartRepository.GetCartItemsByCartIdAsync(cart.CartId);
// if (cartitems is  null)throw new Exception("djhdhdhd");
  

//   var order=new Order
//   {
//     OrderId=Guid.NewGuid(),
//     OrderDate=DateTime.UtcNow,
//     UserId=userId,
//     TotalPrice=0.0m,
//     Status=OrderStatus.AwaitingPayment,
//     OrderItems=new List<OrderItem>()  
//   };

// foreach (var item in cartitems)
// {
//     var product=await _repository.ProductRepository.GetProductAsync(item.ProductId,trackChanges:)


// }
    




//     }



        // private async Task<Order> CreateFromCart(string userId)
        //     {
        //         var cart=await _repository.CartRepository.GetByUserIdAsync(userId);
        //         if(cart is null || !cart.CartItems.Any())throw new Exception("Cart is null");
        // var order=new Order{
        // OrderId=Guid.NewGuid(),
        // UserId=userId,
        // TotalPrice=cart.CartItems.Sum(i=>i.Quantity*i.UnitPrice),
        // OrderDate=DateTime.UtcNow,
        // Status=OrderStatus.AwaitingPayment,
        // OrderItems=cart.CartItems.Select(item=> new OrderItem
        //     {
                
                    
        //         OrderItemId=Guid.NewGuid(),
        //         ProductId=item.ProductId,
        //         Quantity=item.Quantity,
        //         PriceAtPurchase=item.UnitPrice
        //     }).ToList()  
        // };

        // return order; }






            public async Task<OrderDto> CreateOrderAsync(string userId, OrderForCreationDto orderDto,bool trackChanges)
            {
           

           var orderEntity=new Order{
        OrderId=Guid.NewGuid(),
        UserId=userId,
        TotalPrice=orderDto.TotalPrice,
        OrderDate=DateTime.UtcNow,
        Status=OrderStatus.AwaitingPayment,
        OrderItems=new List<OrderItem>() 
        };
      foreach (var item in orderDto.OrderItems)
      {
        var product=await _repository.ProductRepository.GetProductAsync(item.ProductId,trackChanges:false);
        if(product is null || item.Quantity>product.StockQuantity)throw new KeyNotFoundException($"Product with ID {item.ProductId} and Name {product.Name} is not Found Or Exceed the Quantity");

var orderitems=new OrderItem
{
  OrderItemId=Guid.NewGuid(),
  OrderId=orderEntity.OrderId,
  ProductId=item.ProductId,
  Quantity=item.Quantity,
  PriceAtPurchase=product.Price  
};
        orderEntity.OrderItems.Add(orderitems);
      }


  
            _repository.OrderRepository.CreateOrder(userId, orderEntity);
            await _repository.SaveAsync();
            return _mapper.Map<OrderDto>(orderEntity);
            
            }

   





    //     public async Task<OrderDto> CreateOrderAfterPaymentAsync(string userId,Guid cartId,ProcessPaymentForCreation processPaymentDto)
    //     {

    // using var transaction=await _repository.BeginTransactionAsync();

    // try{



    //         var cart=await _repository.CartRepository.GetCartWithItemsAsync(cartId);
    //         if(cart==null || !cart.CartItems.Any())

    //         {
    //             throw new InvalidOperationException("cart is empty");

    //         }

    // decimal total=cart.CartItems.Sum(i=>i.Quantity*i.UnitPrice);

    // var paymentResult=await _payment.ChargeAsync(total,processPaymentDto.paymentMethod);
    // if(!paymentResult.Succeeded) throw new Exception();
    // var payment=new Payment
    // {
    //     PaymentId=Guid.NewGuid(),
    //     Amount=total,

    //     PayMethod=processPaymentDto.paymentMethod,
    //     PayStatus=PaymentStatus.Success,
    //     PayDate=DateTime.UtcNow};



    // await _repository.PaymentRepository.AddAsync(payment);
    // await _repository.SaveAsync();
    // var order=new Order
    // {
    //   OrderId=Guid.NewGuid(),
    //   UserId=userId,
    //   TotalPrice=total,
    //   OrderDate=DateTime.UtcNow,
    //   Status=OrderStatus.Confirmed,
    //   PaymentId=payment.PaymentId,
    //   OrderItems=cart.CartItems.Select(item=> new OrderItem
    //   {
    //       OrderItemId=Guid.NewGuid(),
    //       ProductId=item.ProductId,
    //       Quantity=item.Quantity,
    //       PriceAtPurchase=item.UnitPrice
    //   }).ToList() 

    // };

    // _repository.OrderRepository.CreateOrder(userId,order);

    // foreach(var item in cart.CartItems)
    //         {
    //             var product=await _repository.ProductRepository.GetProductAsync(item.ProductId,trackChanges:true);
    // if (product == null)
    //                 throw new Exception("Product not found");


    //             if (product.StockQuantity < item.Quantity)
    //                 throw new InvalidOperationException(
    //                     $"Insufficient stock for product {product.Name}");

    //             product.StockQuantity-=item.Quantity;
    //             _repository.ProductRepository.UpdateProduct(product);

    //         }

    //          await ClearCartItem(cartId);

    //        await   _repository.SaveAsync();
    // await transaction.CommitAsync();


    //        return _mapper.Map<OrderDto>(order);
    // } catch{

    // await transaction.RollbackAsync();
    // throw;
    // } 
    //     }



    // [Obsolete("use CreateOrderAsync instead ")]
    // public async Task<OrderDto> CreateOrderAsync(string userId, OrderForCreationDto orderDto, bool trackChanges)
    // {

    //     var orderEntity = new Order
    //     {
    //         OrderId = Guid.NewGuid(),
    //         UserId = userId,
    //         TotalPrice =0.0m,
    //         OrderDate = DateTime.UtcNow,
    //         Status = OrderStatus.Pending,
    //         OrderItems = new List<OrderItem>()
    //     };

    //     orderEntity.TotalPrice=orderEntity.OrderItems.Sum(i=>i.Quantity*i.PriceAtPurchase);


    //     foreach (var itemDto in orderDto.OrderItems)
    //     {

    //         var product = await _repository.ProductRepository.GetProductAsync(itemDto.ProductId, false);
    //         if (product == null)
    //             throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} not found");

    //         var orderItem = new OrderItem
    //         {
    //             OrderItemId = Guid.NewGuid(),
    //             OrderId = orderEntity.OrderId,
    //             ProductId = itemDto.ProductId,
    //             Quantity = itemDto.Quantity,
    //             PriceAtPurchase = product.Price
    //         };
    //         // orderEntity.TotalPrice+=orderItem.PriceAtPurchase*orderItem.Quantity;

    //         orderEntity.OrderItems.Add(orderItem);

    //         // Update product stock
    //         product.StockQuantity -= itemDto.Quantity;
    //         if (product.StockQuantity < 0)
    //             throw new InvalidOperationException($"Insufficient stock for product {product.Name}");

    //         _repository.ProductRepository.UpdateProduct(product);
    //     }

    //     // Save order
    //     _repository.OrderRepository.CreateOrder(userId, orderEntity);
    //     await _repository.SaveAsync();

    //     // Return the created order
    //     var entityToReturn = _mapper.Map<OrderDto>(orderEntity);
    //     return entityToReturn;
    // }

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

  



        
}