using Core.Entities;


namespace Core.Contracts;


public interface IPaymentRepository{
  
  Task<Payment> GetPaymentAsync(Guid PaymentId,bool trackChanges);

  Task<IEnumerable<Payment>> GetPaymentsAsync(bool trackChanges);
  Task AddAsync(Payment payment);
  //void  CreatePayment(Payment Payment);
   void  UpdatePayment(Payment Payment);
    void DeletePayment(Payment Payment);



}
