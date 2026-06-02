

using System.Collections.Concurrent;
using System.Text.Json;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Entities.Exceptions;
using Core.Enum.OrderStatus;
using Core.Enum.PaymentMethod;
using Core.Enum.PaymentStatus;
using Core.Enum.TransactionType;
using Core.Shared.DataTransferObjects;
using Core.Shared.Externals;
using Service.Contracts;

namespace Service;



public sealed class PaymentService : IPaymentService
{

    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private static readonly ConcurrentDictionary<Guid ,SemaphoreSlim> _stock=new ();

    
    public PaymentService(IRepositoryManager repository,IMapper mapper)
    {
        _repository=repository;
        _mapper=mapper;
    }

    public async Task ConfirmPaymentAsync(string externalPaymentId)
    {
            var payment = await _repository.PaymentRepository.GetPaymentAsync(Guid.Parse(externalPaymentId), true);
        if (payment == null)
        throw new Exception($"Payment {externalPaymentId} not found");
        if (payment.PayStatus != PaymentStatus.Pending)
        return;

     var order=await _repository.OrderRepository.GetOrderWithItemsAsync(payment.OrderId);
     if (order==null)throw new Exception("Not Found ");
     
    foreach (var orderitem in order.OrderItems)
    {
   var semphore=_stock.GetOrAdd(orderitem.ProductId,_ =>new SemaphoreSlim(1,1));
   await semphore.WaitAsync();
   try{

        var product=await _repository.ProductRepository.GetProductAsync(orderitem.ProductId,true);
        if(product.StockQuantity<orderitem.Quantity){throw new Exception("Quantity Exceed the Stock ");}
product.StockQuantity-=orderitem.Quantity;
_repository.ProductRepository.UpdateProduct(product);
            }
            finally
            {
                semphore.Release();
                _stock.TryRemove(orderitem.ProductId,out _);
            }


    }



        payment.PayStatus = PaymentStatus.Success;
        payment.PaidAt = DateTime.UtcNow;

        var transaction = new PaymentTransaction
        {
        Id = Guid.NewGuid(),
        PaymentId = payment.PaymentId,
        type = TransactionType.Capture,
        Amount = payment.Amount,
        ExternalTransactionId = externalPaymentId,
        CreatedAt = DateTime.UtcNow
        };
        _repository.PaymentTransactionRepository.AddAsync(transaction);
        // تحديث حالة الطلب
    order = await _repository.OrderRepository.GetByIdAsync(payment.OrderId, true);
        if (order != null && order.Status == OrderStatus.AwaitingPayment)
        {
        order.Status = OrderStatus.Confirmed;
        _repository.OrderRepository.UpdateOrder(order);
        }


       var cart= await _repository.CartRepository.GetByUserIdAsync(order.UserId);
        _repository.CartRepository.DeleteItem(cart);

        await _repository.SaveAsync();
    }









    public async Task<PaymentDto> CreatePaymentIntentAsync(Guid orderId, string idempotencyKey)
    {
     var existingRecord = await
_repository.IdempotencyRecordRepository.GetByKeyAsync(idempotencyKey);
if (existingRecord != null)
{
return JsonSerializer.Deserialize<PaymentDto>(existingRecord.ResponseJson)!;
}

var order = await _repository.OrderRepository.GetByIdAsync(orderId, true);
if (order == null)
  throw new Exception($"Order {orderId} not found");

if (order.Status == OrderStatus.Paid)
throw new Exception("Order already paid");


var payment = new Payment
{
PaymentId = Guid.NewGuid(),
OrderId = orderId,
Amount = order.TotalPrice,
PayStatus = PaymentStatus.Pending,
PayMethod = PaymentMethods.CreditCard,
IdempotencyKey = idempotencyKey,
CreatedAt = DateTime.UtcNow
};
await _repository.PaymentRepository.AddAsync(payment);
await _repository.SaveAsync();

var responseDto = _mapper.Map<PaymentDto>(payment);
var idempotencyRecord = new IdempotencyRecord
{
Id = Guid.NewGuid(),
Key = idempotencyKey,
ResponseJson = JsonSerializer.Serialize(responseDto),
CreatedAt = DateTime.UtcNow
};
 _repository.IdempotencyRecordRepository.Add(idempotencyRecord);
await _repository.SaveAsync();
return responseDto;


    }

    public async Task FailPaymentAsync(string externalPaymentId)
    {
        var payment = await
_repository.PaymentRepository.GetPaymentAsync(Guid.Parse(externalPaymentId), true);
if (payment == null || payment.PayStatus != PaymentStatus.Pending)
return;
payment.PayStatus = PaymentStatus.Failed;
var transaction = new PaymentTransaction
{
Id = Guid.NewGuid(),
PaymentId = payment.PaymentId,
type = TransactionType.Failed,
Amount = payment.Amount,
ExternalTransactionId = externalPaymentId,
CreatedAt = DateTime.UtcNow
};
 _repository.PaymentTransactionRepository.AddAsync(transaction);
await _repository.SaveAsync();

    }

    public async Task<PaymentDto> GetPaymentAsync(Guid paymentId)
    {
  var payment = await _repository.PaymentRepository.GetPaymentAsync(paymentId, false);
if (payment == null)
throw new Exception($"Payment {paymentId} not found");
return _mapper.Map<PaymentDto>(payment);
}
public async Task RefundPaymentAsync(Guid paymentId, decimal? amount = null)
{
var payment = await _repository.PaymentRepository.GetPaymentAsync(paymentId, true);
if (payment == null || payment.PayStatus != PaymentStatus.Success)
throw new Exception("Only successful payments can be refunded");
var refundAmount = amount ?? payment.Amount;
var transaction = new PaymentTransaction
{Id = Guid.NewGuid(),
PaymentId = payment.PaymentId,
type = TransactionType.Refund,
Amount = refundAmount,
ExternalTransactionId = Guid.NewGuid().ToString(),
CreatedAt = DateTime.UtcNow
};
 _repository.PaymentTransactionRepository.AddAsync(transaction);
// إذا كان المبلغ مسترد ًا بالكامل، نغير حالة الدفع والطلب
if (refundAmount >= payment.Amount)
{
payment.PayStatus = PaymentStatus.Refunded;
var order = await _repository.OrderRepository.GetByIdAsync(payment.OrderId, true);
if (order != null) order.Status = OrderStatus.Refunded;
}
await _repository.SaveAsync();
    }

   



    //     public async Task CreatePaymentAsync(PaymentForCreationDto paymentForCreation)
    //     {
    //         var entityToSave=_mapper.Map<Payment>(paymentForCreation);
    //         await _repository.PaymentRepository.AddAsync(entityToSave);
    //         await _repository.SaveAsync();


    //     }

    //     public async Task DeletePayment(Guid paymentId)
    //     {

    //          var entity=await _repository.PaymentRepository.GetPaymentAsync(paymentId,false);
    //         _repository.PaymentRepository.DeletePayment(entity);

    //     }

    //     // public async Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId)
    //     // {
    //     //    var pay=await _repository.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
    //     //    var entityToreturn=_mapper.Map<PaymentDto>(pay);
    //     //    return entityToreturn;
    //     // }

    //     public async Task<PaymentDto> GetPaymentDtoAsync(Guid Id, bool trackChanges)
    //     {
    //      var pay=await  _repository.PaymentRepository.GetPaymentAsync(Id,trackChanges);
    //      var payDto=_mapper.Map<PaymentDto>(pay);
    //         return payDto;
    //     }




    //     public async Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(string  userId)
    //     {
    //          var order=await _repository.OrderRepository.GetUserOrdersAsync(userId,false);
    //          var payIds=order.Select(o=>o.OrderId);

    //     var payments=new List<Payment>();
    //     foreach (var orderId in payIds )
    //     {
    // var payment=await _repository.OrderRepository.GetPaymentByOrderId(orderId);
    // if(payment!=null)
    //     payments.Add(payment);

    //     }
    // var payDtos=_mapper.Map<IEnumerable<PaymentDto>>(payments);
    // return payDtos;



    //     }



}