using Core.Entities;
using Core.Shared.DataTransferObjects;

namespace Service.Contracts;




public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(Guid productId);


    Task<IEnumerable<ProductDto?>> GetProductsByAsync(bool trackChanges);

   Task<int> UpdateQuantityAsync(ProductDto productDto);
   void DeleteProduct(Guid Id);
   void DeleteProductsByCategory(Guid categoryId);
    


    
}
