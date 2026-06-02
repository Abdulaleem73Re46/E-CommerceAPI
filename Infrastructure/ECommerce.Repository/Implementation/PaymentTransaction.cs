


using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;



public class PaymentTransactionRepository : RepositoryBase<PaymentTransaction>,
IPaymentTransactionRepository
{
public PaymentTransactionRepository(RepositoryContext context) : base(context) { }

    public  void  AddAsync(PaymentTransaction paymentTransaction)
    {
        
        Create(paymentTransaction);
        
    }

    public async Task<PaymentTransaction?> GetByExternalTransactionIdAsync(string
externalId)
=> await FindByCondition(t => t.ExternalTransactionId.Equals(externalId) , false).FirstOrDefaultAsync();
public async Task<IEnumerable<PaymentTransaction>> GetByPaymentIdAsync(Guid
paymentId)
=> await FindByCondition(t => t.PaymentId.Equals(paymentId), false)
.OrderBy(t => t.CreatedAt).ToListAsync();
}