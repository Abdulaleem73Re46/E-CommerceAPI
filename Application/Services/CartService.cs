
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

    public async Task<CartDto?> GetCartAsync(Guid CartId)
    {
       var cart= await _repository.CartRepository.GetCartAsync(CartId,false);
      var cartdto=_mapper.Map<CartDto>(cart);
       return cartdto;
      


    }

    public async Task<CartDto?> GetCartByUserIdAsync(Guid userId)
    {
       var cart=await _repository.CartRepository.GetByUserIdAsync(userId);
       var cartdto=_mapper.Map<CartDto>(cart);
       return cartdto;
    }

    public async Task<IEnumerable<CartItemDto?>> GetCartItemsByCartIdAsync(Guid CartId)
    {
      //var cart=_repository.CartRepository.GetCartAsync(CartId,false);
   // var cartitem=ca
    
    var cartitems=await _repository.CartRepository.GetCartWithItemsAsync(CartId);
  var cartDto=_mapper.Map<IEnumerable<CartItemDto>>(cartitems);
  return cartDto;

    }

    public void UpdateCart(CartDto cartDto)
    {
        throw new NotImplementedException();
         
        
    }
}
