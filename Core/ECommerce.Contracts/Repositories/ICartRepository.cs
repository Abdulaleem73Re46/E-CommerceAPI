using Core.Entities;

namespace Core.Contracts;


public interface ICartRepository{
  
  Task<Cart> GetCartAsync(Guid CartId,bool trackChanges);

  Task<IEnumerable<Cart>> GetCartsAsync(bool trackChanges);
  Task<Cart?> GetByUserIdAsync(string userId);
  //Task<Cart?> GetCartWithItemsAsync(Guid userId);// it does Include for CartItems And Products 
  void    AddAsync(Cart cart);
    void  UpdateCart(Cart cart);
    //Task<bool> ExistsAsync(Guid userId);// if the user has Cart
    void   DeleteItem(Cart cart);
    void DeleteItem(CartItem cartItem);
    Task<Cart?> GetCartWithItemsAsync(Guid cartId);
   //Task<IQueryable<CartItem>> GetCartItemsAsync(Guid cartId);

   Task<CartItem?> GetCartItemAsync(Guid cartid,Guid poductId);

 Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
 Task AddCartItemAsync(CartItem cartItem);
 Task UpdateCartItemAsync(CartItem cartItem);


}
