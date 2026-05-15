using Core.Entities;
using Core.Shared.DataTransferObjects;
namespace Service.Contracts;




public interface ICartService
{

    Task<CartDto?> GetCartByUserIdAsync(string userId);
    Task<IEnumerable<CartItemDto>> GetCartItemsByCartIdAsync(Guid CartId);
  
     
     Task<CartDto?>  GetCartAsync(Guid CartId);


    Task   DeleteCartByUserId(string userId);

     Task<bool> DeleteItemAsync(Guid CartId);
     Task<bool> DeleteAllItemAsync(Guid cartId);
     Task  UpdateCart(CartDto cartDto);

     Task IncreaseQuantityAsync(UpdateCartItemQuantityDto cartDto);
      Task DecreaseQuantityAsync(UpdateCartItemQuantityDto cartDto);
     

     Task<CartItemDto> AddCartItemByCartIdAsync(Guid CartId,CartItemForCreation cartitemdto);


Task<CartItemDto> UpdateCartItemByCartIdAsync(Guid CartId,CartItemForUpdateDto cartitemdto);

   
   Task<CartItemDto> GetCartItemByCartIdAsync(Guid CartId,Guid productId);
   Task AddCartItemAsync(Guid CartId,CartItemForCreation cartitemdto);


    
}
