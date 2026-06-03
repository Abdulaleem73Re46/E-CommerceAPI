
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;



[Route("api/Payments")]
[ApiController]
[Authorize]
 public class PaymentController : ControllerBase
{
    private readonly IServiceManager _service;
 
 public PaymentController(IServiceManager service)
 {
    _service=service;
    
 }



// [HttpGet("{PaymentId:guid}")]
// public async Task<IActionResult> GetPayment(Guid PaymentId)
//     {
//         var Payment=await _service.PaymentService.GetPaymentDtoAsync(PaymentId,trackChanges:false);
//         return Ok(Payment);
//     }

    
// [HttpGet("user/{userId}")]
// public async Task<IActionResult> GetPaymentByUserId(string userId)
//     {
//         var Payment=await _service.PaymentService.GetPaymentsByUserIdAsync(userId);
//         return Ok(Payment);

//     }



// [HttpGet("{PaymentId:guid}/items")]
// public async Task<IActionResult> GetPaymentItems(Guid PaymentId)
//     {
//         var Payment=await _service.PaymentService.GetPaymentDtoAsync(PaymentId,trackChanges:false);

//         return Ok(Payment);
//     }



[HttpPost("create-Intent/{OrderId:guid}")]
     public async Task<IActionResult> CreatePaymentIntent([FromQuery] Guid OrderId,[FromHeader(Name ="Idempotency-Key")] string IdempotencyKey)
    {

// var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
// if(await _service.OrderService.GetOrdersByUserId(userId,false)=null )

        if (string.IsNullOrWhiteSpace(IdempotencyKey))
        {
            return BadRequest(new {Error="Idempotency-Key header is required "});

        }


        var paymentIntent=await _service.MockPaymentService.CreatePaymentIntentAsync(OrderId,IdempotencyKey);
return Ok(paymentIntent);
        
     


    }






}

