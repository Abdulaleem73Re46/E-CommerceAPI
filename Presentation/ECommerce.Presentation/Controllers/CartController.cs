
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Core.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;



[Route("api/carts")]
[ApiController]
 public class CartController : ControllerBase
{
    private readonly IServiceManager _service;
 
 public CartController(IServiceManager service)
 {
    _service=service;
    
 }



[HttpGet("cartId:guid")]
public async Task<IActionResult> GetCart(Guid cartId)
    {
        var cart=await _service.CartService.GetCartAsync(cartId);
        return Ok(cart);
    }
    
[HttpGet("user/{userId:string}")]
public async Task<IActionResult> GetCartByUserId(string userId)
    {
        var cart=await _service.CartService.GetCartByUserIdAsync(userId);
        return Ok(cart);

    }
[HttpGet("{cartId:guid}/items")]
public async Task<IActionResult> GetCartItems(Guid cartId)
    {
        var cartitems=await _service.CartService.GetCartItemsByCartIdAsync(cartId);
        return Ok(cartitems);
    }

[HttpPost("{cartId:guid}/items")]
public async Task<IActionResult> AddCartItem(Guid cartId, [FromBody] CartItemForCreation cartItemDto)
{
    if (cartItemDto.Quantity <= 0)
        return BadRequest("Quantity must be greater than 0");
    
    var cartItem = await _service.CartService.AddCartItemByCartIdAsync(cartId, cartItemDto);
    return CreatedAtAction(nameof(GetCartItems), new { cartId }, cartItem);
}




}

