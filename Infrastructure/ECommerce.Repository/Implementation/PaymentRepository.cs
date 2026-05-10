using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;



public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
{
    public PaymentRepository(RepositoryContext repository) : base(repository)
    {
    }

   

    public void DeletePayment(Payment Payment)
    {
       Delete(Payment);
    }

    public async Task<Payment> GetPaymentAsync(Guid PaymentId, bool trackChanges)
    {
        return await FindByCondition(p=>p.PaymentId.Equals(PaymentId),trackChanges)
        .SingleOrDefaultAsync();

    }

    public async Task<IEnumerable<Payment>> GetPaymentsAsync(bool trackChanges)
    {
    return await FindAll(trackChanges).OrderBy(p=>p.PayDate).ToListAsync();
    }

  public  Task  AddAsync(Payment payment)
    {
        
        throw new NotImplementedException();
    }

    public void UpdatePayment(Payment Payment)
    {
        Update(Payment);
    }
}