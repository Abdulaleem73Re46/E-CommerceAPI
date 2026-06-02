using Core.Enum.PaymentMethod;
using Core.Shared.Externals;
namespace Service.Contracts;

public interface IPaymentGateway
{
    Task<PaymentResult> ChargeAsync(decimal amount,PaymentMethods paymentMethod,CancellationToken token=default);
}