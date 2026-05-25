

using System.Runtime.CompilerServices;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Features;
using Microsoft.Extensions.Caching.Memory;
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
       
        //   if(_cache.TryGetValue("product",out Product product))
        // {   Console.WriteLine("Data from Cache .....");
        //     var cached=_mapper.Map<ProductDto>(product);
        //     return cached;
        // }


        // Option 1: If product might not exist, throw an exception
         Console.WriteLine("Data from DB .....");
     var   product = await _repository.ProductRepository.GetProductAsync(productId, false);
        if (product == null)
        {
            throw new Exception($"Product with ID {productId} not found");
        }
//_cache.Set("product",product,TimeSpan.FromMinutes(3));
        return _mapper.Map<ProductDto>(product);
    }



    
    public async Task<IEnumerable<ProductDto?>> GetProductsByCategoryIdAsync(Guid catId,ProductParameter productParameters,bool trackChanges)
    {
        var products=await _repository.ProductRepository.GetProductsByCategoryIdAsync(catId,productParameters,trackChanges);
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

     Console.WriteLine(" in creating....");

    
 var productEntity=_mapper.Map<Product>(productDto);
 Console.WriteLine("after mapping....");
productEntity.ProductId=Guid.NewGuid();
 _repository.ProductRepository.CreateProduct(productEntity);
  Console.WriteLine("after creating in repo ....");
await _repository.SaveAsync();
 Console.WriteLine("after saving in service layer....");
  
  

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

    public async Task<(UpdateProductDto partialUpdateProductDto, Product ProductEntity)> PartialUpdateProductAsync(Guid ProductId, bool Track)
    {
        var productEntity=await _repository.ProductRepository.GetProductAsync(ProductId,Track);
        
        if(productEntity is null)throw new KeyNotFoundException($"product with Id {ProductId} not found!");

        var partial=_mapper.Map<UpdateProductDto>(productEntity);
        return (partial,productEntity);





    }

    public async Task SavePatchAsync(UpdateProductDto updateProductDto, Product product)
    {
        _mapper.Map(updateProductDto,product);
        await _repository.SaveAsync();
    }




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
