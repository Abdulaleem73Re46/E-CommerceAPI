
using Core.Entities;
using Core.Shared.DataTransferObjects;


// namespace Service.Contracts;




// public interface IPaymentService
// {
    
//    // Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId);
//    Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(string userId);

//    Task CreatePaymentAsync(PaymentForCreationDto paymentForCreation);
//    Task DeletePayment(Guid paymentId);
//    Task<PaymentDto> GetPaymentDtoAsync(Guid Id,bool trackChanges); 

    
// }










namespace Service.Contracts;
public interface IPaymentService
{
Task<PaymentDto> CreatePaymentIntentAsync(Guid orderId, string idempotencyKey);
Task ConfirmPaymentAsync(string externalPaymentId);
Task FailPaymentAsync(string externalPaymentId);
Task<PaymentDto> GetPaymentAsync(Guid paymentId);
Task RefundPaymentAsync(Guid paymentId, decimal? amount = null);
}