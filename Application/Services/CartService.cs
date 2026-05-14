
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
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

    public async  Task DecreaseQuantityAsync(UpdateCartItemQuantityDto cartDto)
    {
       
        var item=await _repository.CartRepository.GetCartItemAsync(cartDto.CartId,cartDto.ProductId);
        if(item is null)
        {
            throw new Exception("Item not Found!");

        }
        if (item.Quantity > 1)
        {
             item.Quantity--;
        }
        else
        {
            _repository.CartRepository.DeleteItem(item);


        }
        
        await _repository.SaveAsync();


       
    }

    

    public async Task<bool> DeleteAllItemAsync(Guid cartId)
    {
       var cart=await _repository.CartRepository.GetCartAsync(cartId,false);
     if(cart is null)throw new KeyNotFoundException($"Cart with Id {cartId} is not found ");
   foreach(var item in cart.CartItems)
        {
            
_repository.CartRepository.DeleteItem(cart);
        }



await _repository.SaveAsync();
return true;
    }

    public  async Task  DeleteCartByUserId(Guid userId)
    {

        var cart=await  _repository.CartRepository.GetByUserIdAsync(userId);
        if(cart is null)throw new KeyNotFoundException($"Cart for UserId {userId} is not found ");


        _repository.CartRepository.DeleteItem(cart);

       
    }

    public async Task<bool> DeleteItemAsync(Guid CartId)
    {
            var item=await _repository.CartRepository.GetCartAsync(CartId,trackChanges:false);
            if(item is null)throw new KeyNotFoundException($"Cart with  {CartId} is not found ");
     
       _repository.CartRepository.DeleteItem(item);
       await _repository.SaveAsync();
       return true;


       
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

    public async Task<IEnumerable<CartItemDto>> GetCartItemsByCartIdAsync(Guid CartId)
    {
      //var cart=_repository.CartRepository.GetCartAsync(CartId,false);
   // var cartitem=ca
    
    var cartitems=await _repository.CartRepository.GetCartWithItemsAsync(CartId);
  var cartDto=_mapper.Map<IEnumerable<CartItemDto>>(cartitems);
  return cartDto;

    }

    public async Task IncreaseQuantityAsync(UpdateCartItemQuantityDto cartDto)
    {
    var cart=await _repository.CartRepository.GetCartItemAsync(cartDto.CartId,cartDto.ProductId);
    if (cart is null)throw new Exception("Not Fond");

    cart.Quantity++;
  
  await _repository.SaveAsync();


    }

    public async Task UpdateCart(CartDto cartDto)
    {

        var cart=await _repository.CartRepository.GetCartAsync(cartDto.CartId,false);
        if(cart is null)throw new Exception("Not Found");

      _mapper.Map(cartDto,cart);
   
      await _repository.SaveAsync();


        //_repository.CartRepository.UpdateCart()
         
        
    }

    // Task ICartService.DeleteCartByUserId(Guid userId)
    // {
    //     throw new NotImplementedException();
    // }
}
