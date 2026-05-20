using Core.Enum.PaymentMethod;
using Core.Shared.Externals;
namespace Service.Contracts;

public interface IPaymentGateway
{
    Task<PaymentResult> ChargeAsync(decimal amount,PaymentMethod paymentMethod,CancellationToken token=default);
}