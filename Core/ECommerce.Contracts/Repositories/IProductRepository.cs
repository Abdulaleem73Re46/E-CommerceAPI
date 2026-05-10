using Core.Entities;


namespace Core.Contracts;


public interface IProductRepository{
  
  
Task<Product> GetProductAsync(Guid ProductId,bool trackChanges);

  Task<IEnumerable<Product>> GetProductsAsync(bool trackChanges);

  void  CreateProduct(Product Product);
    void  UpdateProduct(Product Product);
    void DeleteProduct(Product Product);


}
