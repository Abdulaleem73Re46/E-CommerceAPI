

using AutoMapper;
using Core.Contracts;
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

    public Task CreatePaymentAsync(PaymentForCreationDto paymentForCreation)
    {
        throw new NotImplementedException();
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

    public Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}