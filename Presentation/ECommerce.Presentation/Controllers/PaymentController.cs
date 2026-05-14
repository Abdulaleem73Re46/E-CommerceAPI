
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Service.Contracts;

namespace ECommerce.Presentation;



[Route("api/Payments")]
[ApiController]
 public class PaymentController : ControllerBase
{
    private readonly IServiceManager _service;
 
 public PaymentController(IServiceManager service)
 {
    _service=service;
    
 }



[HttpGet("PaymentId:guid")]
public async Task<IActionResult> GetPayment(Guid PaymentId)
    {
        var Payment=await _service.PaymentService.GetPaymentDtoAsync(PaymentId,trackChanges:false);
        return Ok(Payment);
    }
    
[HttpGet("user/{userId:string}")]
public async Task<IActionResult> GetPaymentByUserId(string userId)
    {
        var Payment=await _service.PaymentService.GetPaymentsByUserIdAsync(userId);
        return Ok(Payment);

    }
[HttpGet("{PaymentId:guid}/items")]
public async Task<IActionResult> GetPaymentItems(Guid PaymentId)
    {
        var Payment=await _service.PaymentService.GetPaymentDtoAsync(PaymentId,trackChanges:false);

        return Ok(Payment);
    }





}

