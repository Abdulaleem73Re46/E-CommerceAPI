

using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service;



public sealed class PaymentService : IPaymentService
{

    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    public PaymentService(IRepositoryManager repository,IMapper mapper)
    {
        _repository=repository;
        _mapper=mapper;
    }

    public async Task CreatePaymentAsync(PaymentForCreationDto paymentForCreation)
    {
        var entityToSave=_mapper.Map<Payment>(paymentForCreation);
        await _repository.PaymentRepository.AddAsync(entityToSave);
        await _repository.SaveAsync();
        
        
    }

    public async Task DeletePayment(Guid paymentId)
    {

         var entity=await _repository.PaymentRepository.GetPaymentAsync(paymentId,false);
        _repository.PaymentRepository.DeletePayment(entity);

    }

    public async Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId)
    {
       var pay=await _repository.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
       var entityToreturn=_mapper.Map<PaymentDto>(pay);
       return entityToreturn;
    }

    public async Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(string  userId)
    {
         var order=await _repository.OrderRepository.GetUserOrdersAsync(userId,false);
         var payIds=order.Select(o=>o.OrderId);
         
    var payments=new List<Payment>();
    foreach (var orderId in payIds )
    {
var payment=await _repository.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
if(payment!=null)
    payments.Add(payment);
        
    }
var payDtos=_mapper.Map<IEnumerable<PaymentDto>>(payments);
return payDtos;
    
    
    }
}