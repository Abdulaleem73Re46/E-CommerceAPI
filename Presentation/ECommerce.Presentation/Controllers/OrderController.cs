



using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Core.Shared.DataTransferObjects;
using Service;
using Core.Entities;


namespace ECommerce.Presentation;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{

    private readonly IServiceManager _service;
    public OrderController(IServiceManager service)
    {
        _service=service;

    }
[HttpGet("{orderId}")]
public async Task<IActionResult> GetOrder(string orderId)
    {

        if (!Guid.TryParse(orderId, out var guid))
    {
        return BadRequest("Invalid GUID format");
    }

       var or=await _service.OrderService.GetOrderByIdAsync(guid,false);
       return Ok(or);

        


    }


[HttpGet("orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders=await _service.OrderService.GetAllOrdersAsync(false);
        return Ok(orders);
    }

// [HttpPost("create/userId")]
 
// public async Task<IActionResult> CreateOrder(string userId,[FromBody] OrderForCreationDto order)
//     {
//         var createdOrder=await _service.OrderService.CreateOrderAsync(userId,order,false);
//         return CreatedAtAction(nameof(createdOrder), createdOrder);
//     }



    [HttpPost("create/{userId}")]  // Changed from "create/userId" to "create/{userId}"
public async Task<IActionResult> CreateOrder(string userId, [FromBody] OrderForCreationDto order)
{
    if (order == null)
        return BadRequest("Order data is required");
    
    if (order.OrderItems == null || !order.OrderItems.Any())
        return BadRequest("Order must contain at least one item");
    
    try
    {
        var createdOrder = await _service.OrderService.CreateOrderAsync(userId, order, false);
        return CreatedAtAction(nameof(GetOrder),new {OrderId=createdOrder.OrderId}, createdOrder);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}

}