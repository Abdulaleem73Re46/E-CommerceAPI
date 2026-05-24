using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Features;

namespace Service.Contracts;




public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(Guid productId);

   
    Task<IEnumerable<ProductDto?>> GetProductsByCategoryIdAsync(Guid CatId,ProductParameters productParameters,bool trackChanges);

   Task<int> UpdateQuantityAsync(ProductDto productDto);
   Task  DeleteProduct(Guid Id);
   Task DeleteProductsByCategory(Guid categoryId);
    
Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
Task<ProductDto> UpdateProductAsync(Guid productId, UpdateProductDto productDto);


Task<(UpdateProductDto partialUpdateProductDto,Product ProductEntity)> PartialUpdateProductAsync(Guid ProductId,bool Track);

    Task SavePatchAsync(UpdateProductDto updateProductDto,Product product);

    
}
