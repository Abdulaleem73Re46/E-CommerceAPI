
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
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





}

