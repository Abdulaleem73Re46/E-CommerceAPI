using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
{ 
    private readonly RepositoryContext _repo;
    
    public PaymentRepository(RepositoryContext repository) : base(repository)
    {
        _repo = repository;
    }

    public void DeletePayment(Payment Payment)
    {
        Delete(Payment);
    }

    public async Task<Payment> GetPaymentAsync(Guid PaymentId, bool trackChanges)
    {
        return await FindByCondition(p => p.PaymentId.Equals(PaymentId), trackChanges)
            .SingleOrDefaultAsync();
        
    }

    public async Task<IEnumerable<Payment>> GetPaymentsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(p => p.PayDate).ToListAsync();
    }

    public async Task<Payment> GetPaymentByOrderIdAsync(Guid id)
    {
        return await FindByCondition(p => p.OrderId.Equals(id), false)
            .FirstOrDefaultAsync();
    }

    // This matches the interface - returns Task, not Task<Payment>
    public async Task AddAsync(Payment payment)
    {
        if (payment == null)
        {
            throw new ArgumentNullException(nameof(payment));
        }
        
        await _repo.AddAsync(payment);
       
         await _repo.SaveChangesAsync();
    }

    public void UpdatePayment(Payment Payment)
    {
        Update(Payment);
    }
}