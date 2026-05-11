using Core.Entities;
using Core.Shared.DataTransferObjects;
namespace Service.Contracts;




public interface ICartService
{

    Task<CartDto?> GetCartByUserIdAsync(Guid userId);
    Task<IEnumerable<CartItemDto>> GetCartItemsByCartIdAsync(Guid CartId);
  
     
     Task<CartDto?>  GetCartAsync(Guid CartId);


     Task<bool> DeleteCartByUserIdAsync(Guid userId);

     Task<bool> DeleteItemAsync(Guid CartId);
     Task<bool> DeleteAllItemAsync(Guid cartId);
     void UpdateCart(CartDto cartDto);

     



   
    


    
}
