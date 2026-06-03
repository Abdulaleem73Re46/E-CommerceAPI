


// using Core.Contracts;
// using Core.Enum.OrderStatus;
// using Core.Enum.PaymentMethod;
// using Core.Shared.Externals;
// using Core.Shared.Helpers;
// using Microsoft.Extensions.Options;
// using Service.Contracts;

// namespace Service;




// public sealed class PayPalPaymentService : IPaymentGateway
// {

// private readonly PayPalHttpClient _payPalClient;
// private readonly IRepositoryManager _repo;
//             public PayPalPaymentService(IOptions<PayPalSettings> settings, IRepositoryManager repo)
//             {
//             _repo = repo;
//             PayPalEnvironment environment = settings.Value.Mode == "live"
//             ? new LiveEnvironment(settings.Value.ClientId, settings.Value.Secret)
//             : new SandboxEnvironment(settings.Value.ClientId, settings.Value.Secret);
//             // إنشاء كائن العميل الخاص بالحزمة
//             _payPalClient = new PayPalHttpClient(environment);
//             }
//                 public Task CaptureOrderAsync(string paypalOrderId)
//                 {
//                     throw new NotImplementedException();
//                 }

//     public Task<PaymentResult> ChargeAsync(decimal amount, PaymentMethods paymentMethod, CancellationToken token = default)
//     {
//         throw new NotImplementedException();
//     }

//             public async Task<PayPalCreateOrderResponse> CreateOrderAsync(Guid orderId, string
//             idempotencyKey)
//             {
          
//             var existingPayment = await _repo.PaymentRepository.GetByIdempotencyAsync(idempotencyKey,track:false);
//             if (existingPayment != null)
//             {
//             return new PayPalCreateOrderResponse
//             {
//             OrderId = existingPayment.ExternalTransactionId,
//             ApprovalUrl = $"https://www.sandbox.paypal.com/checkoutnow?token={existingPayment.ExternalTransactionId}"
//             };
//             } 
            
//             var order = await _repo.OrderRepository.GetByIdAsync(orderId, true);
//             if (order == null || order.Status != OrderStatus.AwaitingPayment)
//             throw new InvalidOperationException("");


//             var request = new OrdersCreateRequest();
//             request.SetPrefer("return=representation");
//             request.SetPayPalRequestId(idempotencyKey); 
//             request.RequestBody(new OrderRequest
//             {
//             Intent = EOrderIntent.Capture,
//             PurchaseUnits = new List<PurchaseUnitRequest>
//             {
//             new(){
//             ReferenceId = order.Id.ToString(),
//             Amount = new AmountWithBreakdown
//             {
//             CurrencyCode = "USD",
//             Value = order.TotalPrice.ToString("F2")
//             }
//             }
//             },
//             ApplicationContext = new ApplicationContext
//             {
//             ReturnUrl = "https://yourfrontend.com/payment/success",
//             CancelUrl = "https://yourfrontend.com/payment/cancel",
//             UserAction = EUserAction.PayNow
//             }
//             });
//             // إرسال الطلب واستقبال الرد من خلال الحزمة
//             var response = await _payPalClient.ExecuteAsync(request);
//             var paypalOrder = response.Result<Order>();
//             // استخراج رابط الموافقة )(Approval URL
//             var approvalLink = paypalOrder.Links.FirstOrDefault(x => x.Rel == "approve");
//             if (approvalLink == null)
//             ;)". PayPalفشل الحصول على رابط الموافقة من"(throw new Exception
//             // حفظ المعاملة في قاعدة البيانات بحالة معلقة )(Pending
//             var payment = new Payment
//             {
//             Id = Guid.NewGuid(),
//             OrderId = orderId,
//             Amount = order.TotalPrice,
//             Status = PaymentStatus.Pending,
//             PaymentProvider = "PayPal",
//             ExternalTransactionId = paypalOrder.Id,
//             IdempotencyKey = idempotencyKey,
//             CreatedAt = DateTime.UtcNow
//             };
//             await _repo.PaymentRepository.AddAsync(payment);
//             await _repo.SaveAsync();
//             return new PayPalCreateOrderResponse
//             {OrderId = paypalOrder.Id,
//             ApprovalUrl = approvalLink.Href
//             };
//             }

//     public Task RefundAsync(Guid paymentId)
//     {
//         throw new NotImplementedException();
//     }
// }