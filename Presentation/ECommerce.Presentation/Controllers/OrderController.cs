



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
[HttpGet("{orderId:guid}")]
public async Task<IActionResult> GetOrder(Guid orderId)
    {
       var or=await _service.OrderService.GetOrderByIdAsync(orderId,false);
       return Ok(or);

        


    }


}