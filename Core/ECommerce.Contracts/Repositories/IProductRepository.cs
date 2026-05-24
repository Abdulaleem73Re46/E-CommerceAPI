using Core.Entities;
using Core.Shared.Features;


namespace Core.Contracts;


public interface IProductRepository{
  
  
Task<Product> GetProductAsync(Guid ProductId,bool trackChanges);

  Task<IEnumerable<Product>> GetProductsAsync(Guid Id,ProductParameters productParameters,bool trackChanges);

  void  CreateProduct(Product Product);
    void  UpdateProduct(Product Product);
    void DeleteProduct(Product Product);
    Task DeleteProductsByCategoryId(Guid Id);

    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid id,ProductParameters productParameters,bool trackChanges); 


}
