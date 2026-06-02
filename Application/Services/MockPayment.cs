




using Core.Enum.PaymentMethod;
using Core.Shared.Externals;
using Service.Contracts;
namespace Service;
public class MockPayment : IPaymentGateway
{
    
public async Task<PaymentResult> ChargeAsync(decimal amount, PaymentMethods paymentMethod, CancellationToken token=default)
    {
         await Task.Delay(2000,token);
        if (amount <= 0)
        {
            return PaymentResult.Failed("Invalid Amount payment");

        }
        return amount<1000?PaymentResult.Success(transactionId:Guid.NewGuid().ToString()):PaymentResult.Failed("Insufficient Funds");
        
    }



}