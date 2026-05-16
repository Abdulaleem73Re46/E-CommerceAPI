

using System.Runtime.CompilerServices;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service ;


public sealed class ProductService : IProductService
{

    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repository,IMapper mapper)
    {
        
        _repository=repository;
        _mapper=mapper;
    }

    public async Task DeleteProduct(Guid Id)
    {
       var pro= await _repository.ProductRepository.GetProductAsync(Id,false);
       if(pro is null)throw new Exception("Not found");
_repository.ProductRepository.DeleteProduct(pro);
await _repository.SaveAsync();

      



    }

    public async Task DeleteProductsByCategory(Guid categoryId)
    {
       var cate=await _repository.CategoryRepository.GetCategoryAsync(categoryId,false);
       if(cate is null)throw new Exception("not Found");
       
       await _repository.ProductRepository.DeleteProductsByCategoryId(categoryId);
       await _repository.SaveAsync();


    }

   public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
    {
        // Option 1: If product might not exist, throw an exception
        var product = await _repository.ProductRepository.GetProductAsync(productId, false);
        if (product == null)
        {
            throw new Exception($"Product with ID {productId} not found");
        }

        return _mapper.Map<ProductDto>(product);
    }
    public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryIdAsync(Guid catId,bool trackChanges)
    {
        var products=await _repository.ProductRepository.GetProductsByCategoryIdAsync(catId,trackChanges);
        if (products is null )throw new Exception();

      return _mapper.Map<IEnumerable<ProductDto>>(products);

        
          
    }

    public async Task<int> UpdateQuantityAsync(ProductDto productDto)
    {
      var product=await _repository.ProductRepository.GetProductAsync(productDto.ProductId,trackChanges:false);
     if(product is null )throw new KeyNotFoundException($"Product with ID {productDto.ProductId} is not found ");
   
     product.StockQuantity=productDto.StockQuantity;
     _repository.ProductRepository.UpdateProduct(product);
  await    _repository.SaveAsync();
  return product.StockQuantity;



    }





public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto){



 var productEntity=_mapper.Map<Product>(productDto);

productEntity.ProductId=Guid.NewGuid();
 _repository.ProductRepository.CreateProduct(productEntity);
await _repository.SaveAsync();

return _mapper.Map<ProductDto>(productEntity);




} 

public async   Task<ProductDto> UpdateProductAsync(Guid productId, UpdateProductDto productDto){


var product=await _repository.ProductRepository.GetProductAsync(productId,false);
if(product is null)throw new KeyNotFoundException($"Product with id {productId} not found") ;



var productEntity=_mapper.Map<Product>(productDto);
_repository.ProductRepository.UpdateProduct(productEntity);
    await _repository.SaveAsync();
    
    return _mapper.Map<ProductDto>(productEntity);



} 





/*



public record CreateProductDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public Guid? CategoryId { get; init; }
}

public record UpdateProductDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public Guid? CategoryId { get; init; }
}









*/









}