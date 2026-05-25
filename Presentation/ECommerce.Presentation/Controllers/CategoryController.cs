

using System.Runtime.CompilerServices;
using Core.Shared.DataTransferObjects;
using Core.Shared.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;


[Route("api/category")]
[ApiController]
[Authorize(Roles ="admin")]
public class CategoryController:ControllerBase
{
    private readonly IServiceManager _service;


    public CategoryController(IServiceManager service)
    {
        _service=service;

        
    }

[HttpGet("{CategoryId:guid}")]
     public async Task<IActionResult> GetCategory(Guid CategoryId)
    {
        var category=await _service.CategoryService.CategoryAsync(CategoryId,trackChanges:false);
        
        return Ok(category);

    }

[HttpGet]
public async Task<IActionResult> GetCategories([FromQuery] CategoryParameter categoryParameter)
    {
        var categories=await _service.CategoryService.GetCategoriesAsync(categoryParameter,trackChanges:false);
        return Ok(categories);
    }

[HttpPost("create")]
     public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryForCreationDto categoryForCreation)
    {
        var cateCreate=await _service.CategoryService.CreateCategoryDtoAsync(categoryForCreation,trackChanges:true);
        return CreatedAtAction(nameof(GetCategory),new {CategoryId=cateCreate.CategoryId,cateCreate});



    } 

[HttpDelete("delete")]
public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var deleted=await _service.CategoryService.DeleteCategoryAsync(categoryId);
        
        return NoContent();


    }
   







}