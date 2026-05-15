using Core.Entities;
using Core.Shared.DataTransferObjects;

namespace Service.Contracts;




public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(Guid productId);


    Task<IEnumerable<ProductDto?>> GetProductsByCategoryIdAsync(Guid CatId,bool trackChanges);

   Task<int> UpdateQuantityAsync(ProductDto productDto);
   Task  DeleteProduct(Guid Id);
   Task DeleteProductsByCategory(Guid categoryId);
    
Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
Task<ProductDto> UpdateProductAsync(Guid productId, UpdateProductDto productDto);


    
}
