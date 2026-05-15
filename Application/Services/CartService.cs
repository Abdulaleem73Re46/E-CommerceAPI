
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
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

//     public async Task<CartItemDto> AddCartItemAsync(Guid cartId, CartItemForCreation cartItemDto)
// {
//     // Get the cart
//     var cart = await _repository.CartRepository.GetCartAsync(cartId, true);
//     if (cart == null)
//         throw new KeyNotFoundException($"Cart with ID {cartId} not found");
    
//     // Get the product
//     var product = await _repository.ProductRepository.GetProductAsync(cartItemDto.ProductId, false);
//     if (product == null)
//         throw new KeyNotFoundException($"Product with ID {cartItemDto.ProductId} not found");
    
//     // Check if item already exists in cart
//     var existingItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);
    
//     if (existingItem != null)
//     {
//         // Update quantity
//         existingItem.Quantity += cartItemDto.Quantity;
//         _repository.CartRepository.UpdateCart(cart);
//     }
//     else
//     {
//         // Create new cart item
//         var cartItem = new CartItem
//         {   Id = Guid.NewGuid(),
//             CartId = cartId,
//             ProductId = cartItemDto.ProductId,
//             Quantity = cartItemDto.Quantity,
//             UnitPrice = product.Price,
//             AddedAt = DateTime.UtcNow
//         };
        
//    await   _repository.CartRepository.AddCartItemAsync(cartItem);
//     }
    
//     await _repository.SaveAsync();
    
//     // Return the updated cart item
//     var updatedItem = await _repository.CartRepository.GetCartItemAsync(cartId, cartItemDto.ProductId);
//     return _mapper.Map<CartItemDto>(updatedItem);
// }

    public async Task<CartItemDto> AddCartItemByCartIdAsync(Guid CartId, CartItemForCreation cartitemdto)
    {
    var cart= await _repository.CartRepository.GetCartAsync(CartId,false);
    if(cart is null)throw new KeyNotFoundException($"Cart with Id {CartId} not found"); 
    var product=await _repository.ProductRepository.GetProductAsync(cartitemdto.ProductId,false);
    if(product is null)throw new KeyNotFoundException($"Product with Id {cartitemdto.ProductId} not found");
  var cartItem=new CartItem
  {Id=Guid.NewGuid(),       
    CartId=CartId,  
    ProductId=cartitemdto.ProductId,
    Quantity=cartitemdto.Quantity,      
    UnitPrice=product.Price,
    AddedAt=DateTime.UtcNow
  };    
   await  _repository.CartRepository.AddCartItemAsync(cartItem);  
   await  _repository.SaveAsync();
    return _mapper.Map<CartItemDto>(cartItem);   

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

    public  async Task  DeleteCartByUserId(string  userId)
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

    // public async Task<CartDto?> GetCartByUserIdAsync(string userId)
    // {
    //    var cart=await _repository.CartRepository.GetByUserIdAsync(userId);
    //    var cartdto=_mapper.Map<CartDto>(cart);
    //    return cartdto;
    // }

    public async Task<CartDto?> GetCartByUserIdAsync(string userId)
    {
        var cart=await _repository.CartRepository.GetByUserIdAsync(userId);
        if(cart is null)throw new KeyNotFoundException($"User with Id {userId} not found");

        return _mapper.Map<CartDto>(cart);

    }

    public async Task<CartItemDto> GetCartItemByCartIdAsync(Guid CartId, Guid productId)
    {
         var cartitem=await _repository.CartRepository.GetCartItemAsync(CartId,productId);
    if(cartitem is null)throw new KeyNotFoundException($"Cart with Id {CartId} not found");


return _mapper.Map<CartItemDto>(cartitem);
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

    public async Task<CartItemDto> UpdateCartItemByCartIdAsync(Guid CartId, CartItemForUpdateDto cartitemdto)
    {
       var Items=await _repository.CartRepository.GetCartItemsByCartIdAsync(CartId);
  var cartitem=Items.FirstOrDefault(ci=>ci.Id==cartitemdto.Id);
    if(cartitem is null)throw new KeyNotFoundException($"User with Id {cartitemdto.Id} not found");

cartitem.Quantity=cartitemdto.Quantity;
await _repository.SaveAsync();
return _mapper.Map<CartItemDto>(cartitem);

    }

   public async  Task  AddCartItemAsync(Guid CartId, CartItemForCreation cartitemdto)
    {
        var cart=await _repository.CartRepository.GetCartAsync(CartId,false);
        if(cart is null)throw new KeyNotFoundException($"Cart with Id {CartId} not found");

        var product=await _repository.ProductRepository.GetProductAsync(cartitemdto.ProductId,false);
        if(product is null)throw new KeyNotFoundException($"Product with Id {cartitemdto.ProductId} not found");

        var cartItem=new CartItem
        {
            Id=Guid.NewGuid(),
            CartId=CartId,
            ProductId=cartitemdto.ProductId,
            Quantity=cartitemdto.Quantity,
            UnitPrice=product.Price,
            AddedAt=DateTime.UtcNow
        };

       await _repository.CartRepository.AddCartItemAsync(cartItem);
       await _repository.SaveAsync();   
    }



    
}
