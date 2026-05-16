


using Core.Entities;
using Core.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;


[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{

private readonly IServiceManager _service;
public ProductController(IServiceManager service)
{
    _service=service;
    
}
    


   [HttpGet("{productId:guid}")]
   public async Task<IActionResult> GetProduct(Guid productId)
    {
        
   var product=await _service.ProductService.GetProductByIdAsync(productId);
   
     return Ok(product);} 

[HttpGet("products/{categoryId}")]
  public async Task<IActionResult> GetAllProducts(Guid categoryId)
    {
        
    var products=await _service.ProductService.GetProductsByCategoryIdAsync(categoryId,trackChanges:false);
    return Ok(products);




    }




    [HttpPost("create")]
    public async Task<IActionResult>  CreateProduct([FromBody] CreateProductDto productDto)
  {
    if (!productDto.CategoryId.HasValue)
    {
      return BadRequest("category Id is required ");

    }

     var category=await _service.CategoryService.CategoryAsync(productDto.CategoryId.Value,trackChanges:false);
     if(category is null) throw new KeyNotFoundException($"category with Id {productDto.CategoryId} not found ");
       
    
    var createdproduct= await _service.ProductService.CreateProductAsync(productDto);

     return CreatedAtAction(nameof(GetProduct),new {productId=createdproduct.ProductId},createdproduct);

   




  }




}
//D2B3C4D5-E6F7-4A80-9012-345678901BCD
//E3C4D5E6-F7A8-4B90-0123-456789012CDE
//C1A2B3C4-D5E6-4F70-8901-234567890ABC
