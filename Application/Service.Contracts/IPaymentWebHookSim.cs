namespace Service.Contracts;

public interface IPaymentWebhookSimulator
{
Task SimulateSuccessAsync(string externalPaymentId);
Task SimulateFailureAsync(string externalPaymentId);
}