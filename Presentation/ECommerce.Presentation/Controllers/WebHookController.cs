


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;
[Route("api/mock-webhook")]
[ApiController]
    public class MockWebhookController : ControllerBase
    {
    private readonly IPaymentWebhookSimulator _webhook;
    public MockWebhookController(IPaymentWebhookSimulator webhook)
    {
    _webhook = webhook;
    }

    [HttpPost("success/{paymentId}")]
    public async Task<IActionResult> Success(string paymentId)
    {
    await _webhook.SimulateSuccessAsync(paymentId);
    return Ok(new { message = "Mock payment success webhook executed" });
    }

    [HttpPost("fail/{paymentId}")]
    public async Task<IActionResult> Fail(string paymentId)
    {
    await _webhook.SimulateFailureAsync(paymentId);
    return Ok(new { message = "Mock payment failure webhook executed" });
    }}