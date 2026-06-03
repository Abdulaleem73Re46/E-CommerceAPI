

// using System.Text.Json;
// using AutoMapper;
// using Core.Contracts;
// using Core.Entities;
// using Core.Enum.OrderStatus;
// using Core.Enum.PaymentStatus;
// using Core.Shared.DataTransferObjects;
// using Microsoft.Extensions.Options;
// using Service.Contracts;
// using Stripe;
// using Core.Enum.PaymentMethod;
// using Stripe.Forwarding;
// using Core.Enum.TransactionType;
// using Stripe.Events;
// namespace Service;



// public sealed class StripePaymentService : IPaymentService
// {

//    private readonly IRepositoryManager _repository;
//    private readonly IMapper _mapper;
//    private readonly IOptions<StripeSettings> _stripeSettings;


//     public StripePaymentService(IRepositoryManager repository,IMapper mapper,IOptions<StripeSettings> stripeSettings)
//     {

//         _repository=repository;
//         _mapper=mapper;
//         _stripeSettings=stripeSettings;
//         StripeConfiguration.ApiKey=_stripeSettings.Value.SecretKey;

        
//     }
//                 public async Task ConfirmPaymentAsync(string externalPaymentId)
//                 {
                    

//             var paymentIntentService = new PaymentIntentService();
//             var paymentIntent = await paymentIntentService.GetAsync(externalPaymentId);
//             if (paymentIntent.Status == "succeeded")
//             {
            
//             var payment = await
//             _repository.PaymentRepository.GetPaymentAsync(Guid.Parse(externalPaymentId), true);
//             if (payment == null || payment.PayStatus != PaymentStatus.Pending)
//             return;
      
//             payment.PayStatus = PaymentStatus.Success;
//             payment.PaidAt = DateTime.UtcNow;
         
//             var transaction = new PaymentTransaction
//             {
//             Id = Guid.NewGuid(),
//             PaymentId = payment.PaymentId,
//             type = TransactionType.Capture,
//             Amount = payment.Amount,
//             ExternalTransactionId = paymentIntent.Id,
//             CreatedAt = DateTime.UtcNow};
//              _repository.PaymentTransactionRepository.AddAsync(transaction);
          
//             var order = await _repository.OrderRepository.GetOrderWithItemsAsync(payment.OrderId);

//             if (order != null && order.Status == OrderStatus.AwaitingPayment)
//             {
//             foreach (var orderItem in order.OrderItems)
//             {
//             var product = await
//             _repository.ProductRepository.GetProductAsync(orderItem.ProductId, true);
//             if (product.StockQuantity < orderItem.Quantity)
//             throw new InvalidOperationException($"Insufficient stock for product {product.Name}");
//             product.StockQuantity -= orderItem.Quantity;
//             _repository.ProductRepository.UpdateProduct(product);
//             }
//             order.Status = OrderStatus.Confirmed;
//             }
//             var cart=await _repository.CartRepository.GetByUserIdAsync(order.UserId);
//             _repository.CartRepository.DeleteItem(cart);
//             await _repository.SaveAsync();

//             }
//                 }



                
//     public async Task<PaymentDto> CreatePaymentIntentAsync(Guid orderId, string idempotencyKey)
//     {
//         var existingRecord=await _repository.IdempotencyRecordRepository.GetByKeyAsync(idempotencyKey);
//     if(existingRecord!=null) return JsonSerializer.Deserialize<PaymentDto>(existingRecord.ResponseJson)!;

//     var order=await _repository.OrderRepository.GetByIdAsync(orderId,true);
//     if (order == null) throw new KeyNotFoundException($"Order {orderId} not found");
// if (order.Status != OrderStatus.AwaitingPayment)
// throw new InvalidOperationException("Order is not in a payable state.");

// var paymentIntentService = new PaymentIntentService();
// var createOptions = new PaymentIntentCreateOptions
// {
// Amount = (long?)(order.TotalPrice * 100), 
// Currency = "usd",
// Metadata = new Dictionary<string, string> { { "OrderId", orderId.ToString() } }
// };
// var requestOptions=new RequestOptions
// {
//   IdempotencyKey=idempotencyKey  
// };

// var paymentIntent = await paymentIntentService.CreateAsync(createOptions,requestOptions);



// var payment = new Payment
// {
// PaymentId = Guid.NewGuid(),
// OrderId = orderId,
// Amount = order.TotalPrice,
// PayStatus = PaymentStatus.Pending,
// PayMethod =PaymentMethods.Stripe,
// ExternalPaymentId = paymentIntent.Id,
// IdempotencyKey = idempotencyKey,
// CreatedAt = DateTime.UtcNow
// };
// await _repository.PaymentRepository.AddAsync(payment);
// await _repository.SaveAsync();

// var responseDto = _mapper.Map<PaymentDto>(payment);



// var idempotencyRecord = new IdempotencyRecord
// {
// Id = Guid.NewGuid(),
// Key = idempotencyKey,
// ResponseJson = JsonSerializer.Serialize(responseDto),
// CreatedAt = DateTime.UtcNow
// };

//  _repository.IdempotencyRecordRepository.Add(idempotencyRecord);
// await _repository.SaveAsync();
// return responseDto;



// }
       
    

//                 public async Task FailPaymentAsync(string externalPaymentId)
//                 {
//             var payment = await
//             _repository.PaymentRepository.GetPaymentAsync(Guid.Parse(externalPaymentId), true);
//             if (payment == null || payment.PayStatus != PaymentStatus.Pending)
//             return;
//             payment.PayStatus = PaymentStatus.Failed;
//             var transaction = new PaymentTransaction
//             {
//             Id = Guid.NewGuid(),
//             PaymentId = payment.PaymentId,
//             type = TransactionType.Failed,
//             Amount = payment.Amount,
//             ExternalTransactionId = externalPaymentId,
//             CreatedAt = DateTime.UtcNow
//             };
//             _repository.PaymentTransactionRepository.AddAsync(transaction);
//             await _repository.SaveAsync();
//                 }











//     public async Task<PaymentDto> GetPaymentAsync(Guid paymentId)
//     {
//         var payment=await _repository.PaymentRepository.GetPaymentAsync(paymentId,trackChanges:false);
//     return _mapper.Map<PaymentDto>(payment);

//     }

//     public async Task RefundPaymentAsync(Guid paymentId, decimal? amount = null)
//     {
//         var payment = await _repository.PaymentRepository.GetPaymentAsync(paymentId, true);
// if (payment == null || payment.PayStatus != PaymentStatus.Success)
// throw new InvalidOperationException("Only successful payments can be refunded.");
// var refundAmount = amount ?? payment.Amount;
// var refundOptions = new RefundCreateOptions
// {
// PaymentIntent = payment.ExternalPaymentId,
// Amount = (long?)(refundAmount * 100)
// };
// var refundService = new RefundService();
// var refund = await refundService.CreateAsync(refundOptions);
// var transaction = new PaymentTransaction
// {
// Id = Guid.NewGuid(),
// PaymentId = payment.PaymentId,
// type = TransactionType.Refund,
// Amount = refundAmount,
// ExternalTransactionId = refund.Id,
// CreatedAt = DateTime.UtcNow
// };
//  _repository.PaymentTransactionRepository.AddAsync(transaction);
// if (refundAmount >= payment.Amount)
// {
// payment.PayStatus = PaymentStatus.Refunded;
// var order = await _repository.OrderRepository.GetByIdAsync(payment.OrderId, true);
// if (order != null) order.Status = OrderStatus.Refunded;
// }
// await _repository.SaveAsync();
//     }
// }