


using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;



public class CartRepository : RepositoryBase<Cart>, ICartRepository
{
      
    public CartRepository(RepositoryContext repository) : base(repository)
    {
    }

    public void  AddAsync(Cart cart)=> Create(cart);

    // public void CreateCart(Cart cart)
    // {
    //     throw new NotImplementedException();
    // }


    

    public void DeleteItem(Cart cart)=>Delete(cart);

    //public Task<bool> ExistsAsync(Guid userId)

    public async Task<Cart?> GetByUserIdAsync(Guid userId)=>await FindByCondition(c=>c.UserId.Equals(userId),false)
    .SingleOrDefaultAsync();

    public async Task<Cart> GetCartAsync(Guid CartId, bool trackChanges)=>await FindByCondition(c=>c.CartId.Equals(CartId),trackChanges)
    .SingleOrDefaultAsync();

    public async Task<IEnumerable<Cart>> GetCartsAsync(bool trackChanges)=>await FindAll(trackChanges)
    .OrderBy(c=>c.CartId)
    .ToListAsync();

    //public async Task<Cart?> GetCartWithItemsAsync(Guid userId)=>await FindAll(Cart)

    public void UpdateCart(Cart cart)=>Update(cart);

public async Task<Cart?> GetCartWithItemsAsync(Guid cartId)=> await FindByCondition(c=>c.CartId.Equals(cartId),false).Include(c=>c.CartItems).ThenInclude(ci=>ci.Product).SingleOrDefaultAsync();


  //public async Task<IQueryable<Cart>> GetCartItemsAsync(Guid cartId)=> await FindByCondition(c=>c.CartId.Equals(cartId),false).SingleOrDefaultAsync();


   

    


}



//  Task<Cart> GetCartAsync(Guid CartId,bool trackChanges);

//   Task<IEnumerable<Cart>> GetCartsAsync(bool trackChanges);
//   Task<Cart?> GetByUserIdAsync(Guid userId);
//   Task<Cart?> GetCartWithItemsAsync(Guid userId);// it does Include for CartItems And Products 
//   void    AddAsync(Cart cart);
//     void  UpdateCart(Cart cart);
//     Task<bool> ExistsAsync(Guid userId);// if the user has Cart
//     void DeleteItem(Cart cart);