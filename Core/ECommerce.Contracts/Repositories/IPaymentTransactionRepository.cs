



using Core.Entities;
namespace Core.Contracts;
public interface IPaymentTransactionRepository : IRepositoryBase<PaymentTransaction>
{
Task<PaymentTransaction?> GetByExternalTransactionIdAsync(string externalId);
Task<IEnumerable<PaymentTransaction>> GetByPaymentIdAsync(Guid paymentId);
void AddAsync(PaymentTransaction paymentTransaction);

}



