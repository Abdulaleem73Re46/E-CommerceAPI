using Core.Enum.PaymentMethod;
using Core.Shared.DataTransferObjects;
using Core.Shared.Externals;
namespace Service.Contracts;

public interface IPaymentGateway

{
Task<PaymentDto> CreatePaymentIntentAsync(Guid orderId, string idempotencyKey);
Task ConfirmPaymentAsync(string externalPaymentId);
Task FailPaymentAsync(string externalPaymentId);
Task RefundPaymentAsync(Guid paymentId, decimal? amount = null);
Task<PaymentDto> GetPaymentAsync(Guid paymentId);
}


