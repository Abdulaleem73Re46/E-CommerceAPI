
using Core.Entities;
using Core.Shared.DataTransferObjects;


namespace Service.Contracts;




public interface IPaymentService
{
    
   Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId);
   Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(string userId);

   Task CreatePaymentAsync(PaymentForCreationDto paymentForCreation);
   Task DeletePayment(Guid paymentId);

    
}
