
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Core.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;



[Route("api/carts")]

[ApiController]
[Authorize]
 public class CartController : ControllerBase
{
    private readonly IServiceManager _service;
 
 public CartController(IServiceManager service)
 {
    _service=service;
    
 }



[HttpGet("{cartId:guid}")]
public async Task<IActionResult> GetCart(Guid cartId)
    {
        var cart=await _service.CartService.GetCartAsync(cartId);
        return Ok(cart);
    }
    
[HttpGet("user/{userId}")]
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

[HttpPost("{userId}")]

 public async  Task<IActionResult> CreateCart(string userId)
    {
        
     var cart=await _service.CartService.CreateCart(userId);
       return CreatedAtAction(nameof(GetCart),new {CartId=cart.CartId}, cart);



    }



[HttpPost("{cartId:guid}/items")]
public async Task<IActionResult> AddCartItem(Guid cartId, [FromBody] CartItemForCreation cartItemDto)
{
    if (cartItemDto.Quantity <= 0)
        return BadRequest("Quantity must be greater than 0");
    
    var cartItem = await _service.CartService.AddCartItemByCartIdAsync(cartId, cartItemDto);
    return CreatedAtAction(nameof(GetCartItems), new { cartId }, cartItem);
}


// public async Task<IActionResult> UpdateCart(CartForUpdateDto cartForUpdateDto)



[HttpPatch("{cartItemId:guid}")]
public async Task<IActionResult> UpdateByPatchCart(Guid cartId,Guid cartItemId,[FromBody] JsonPatchDocument<CartItemForUpdateDto> jsonPatch)
    {
        
    if (jsonPatch is null) return BadRequest("patch obj is null");
    var result=await _service.CartService.GetCartItemForPatch(cartId,cartItemId,track:true);
    jsonPatch.ApplyTo(result.cartItemForPatchUpdate);
    
    await _service.CartService.SaveChangesForPatch(result.cartItemForPatchUpdate,result.CartItemEntity);


return NoContent();
    }
    


[HttpPost("checkout/{userId:string}")]
    public async Task<IActionResult> CheckOutCart(string userId)
    {
    var orderDto=await _service.CartService.TransformToOrderAsync(userId);

var createdOrder=await _service.OrderService.CreateOrderAsync(userId,orderDto,trackChanges:true);
return Ok(new{ orderId=createdOrder.OrderId,status=createdOrder.Status});


    }

}

