
using Core.Entities;
using Core.Shared.DataTransferObjects;


namespace Service.Contracts;




public interface IPaymentService
{
    
   Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId);
   Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(Guid userId);

   Task CreatePaymentAsync(PaymentForCreation paymentForCreation);
   void DeletePayment(Guid paymentId);

    
}
