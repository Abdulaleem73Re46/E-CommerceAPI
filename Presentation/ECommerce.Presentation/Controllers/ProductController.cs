


using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using Service.Contracts;

namespace ECommerce.Presentation;


[Route("api/products")]
[ApiController]


public class ProductController : ControllerBase
{

private readonly IServiceManager _service;

private IMemoryCache _cache;

public ProductController(IServiceManager service,IMemoryCache cache)
{
    _service=service;
    _cache=cache;

}
    


   [HttpGet("{productId:guid}")]
   public async Task<IActionResult> GetProduct(Guid productId)
    {
        string cacheKey="product";
        if(_cache.TryGetValue(cacheKey,out ProductDto product))
    {
      Console.WriteLine("Data From  cache");
      return Ok(product);
    }
     Console.WriteLine("Data From  DB");
    product=await _service.ProductService.GetProductByIdAsync(productId);
   
    _cache.Set(cacheKey,product,TimeSpan.FromMinutes(2));



     return Ok(product);
     } 

[HttpGet("products/{categoryId}")]
[EnableRateLimiting("FixedWindowRateLimiting")]
  public async Task<IActionResult> GetAllProducts(Guid categoryId,[FromQuery] ProductParameters productParameters)
    {
        string cacheKey="products";
        if(_cache.TryGetValue(cacheKey,out IEnumerable<ProductDto> products))
    {
      return Ok(products);
    }

    products=await _service.ProductService.GetProductsByCategoryIdAsync(categoryId,productParameters,trackChanges:false);
    
    _cache.Set(cacheKey,products,TimeSpan.FromMinutes(3));

    return Ok(products);




    }




    [HttpPost("create")]
    public async Task<IActionResult>  CreateProduct([FromBody] CreateProductDto productDto)
  {
    Console.WriteLine("TTTTTTTTTTT....");
    // if (!productDto.CategoryId.HasValue)
    // {
    //   return BadRequest("category Id is required ");

    // }

     //var category=await _service.CategoryService.CategoryAsync(productDto.CategoryId.Value,trackChanges:false);
     //if(category is null) throw new KeyNotFoundException($"category with Id {productDto.CategoryId} not found ");
       
    
    var createdproduct= await _service.ProductService.CreateProductAsync(productDto);
 Console.WriteLine("00000000000....");
     return CreatedAtAction(nameof(GetProduct),new {productId=createdproduct.ProductId},createdproduct);

  }

[HttpPatch("{productId:guid}")]
  public async Task<IActionResult> PartialUpdate(Guid productId,[FromBody] JsonPatchDocument<UpdateProductDto> jsonPatchDocument)
  {
    if(jsonPatchDocument is null )return BadRequest("Json Patch Product is null");
    var result=await _service.ProductService.PartialUpdateProductAsync(productId,true);

    jsonPatchDocument.ApplyTo(result.partialUpdateProductDto);

    await _service.ProductService.SavePatchAsync(result.partialUpdateProductDto,result.ProductEntity);
    return NoContent();



  }











}
//D2B3C4D5-E6F7-4A80-9012-345678901BCD      care
//E3C4D5E6-F7A8-4B90-0123-456789012CDE
//C1A2B3C4-D5E6-4F70-8901-234567890ABC
//http://localhost:5276/api/products/products/C1A2B3C4-D5E6-4F70-8901-234567890ABC
