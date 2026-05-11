
using AutoMapper;
using Core.Contracts;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service;




public sealed class CartService : ICartService
{

    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    public CartService(IRepositoryManager repository,IMapper mapper)
    {
        _repository=repository;
        _mapper=mapper;
    }

    public Task<bool> DeleteAllItemAsync(Guid cartId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCartByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteItemAsync(Guid CartId)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto?> GetCartAsync(Guid CartId)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto?> GetCartByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CartItemDto>> GetCartItemsByCartIdAsync(Guid CartId)
    {
        throw new NotImplementedException();
    }

    public void UpdateCart(CartDto cartDto)
    {
        throw new NotImplementedException();
    }
}
