

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

    public void DeletePayment(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDto> GetPaymentByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}