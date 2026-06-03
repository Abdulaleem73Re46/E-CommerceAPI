




using Service.Contracts;
namespace Service;
public sealed class MockPaymentWebhookSimulator : IPaymentWebhookSimulator
{
private readonly IPaymentGateway _paymentGateway;
public MockPaymentWebhookSimulator(IPaymentGateway paymentGateway)
{
_paymentGateway = paymentGateway;
}

public async Task SimulateSuccessAsync(string externalPaymentId)
{
 await Task.Delay(2000);
await _paymentGateway.ConfirmPaymentAsync(externalPaymentId);
}
// 🔥 محاكاة فشل الدفع
public async Task SimulateFailureAsync(string externalPaymentId)
{
     await Task.Delay(2000);
await _paymentGateway.FailPaymentAsync(externalPaymentId);
}}
