


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;
[Route("/api/webhook-stripe")]
[ApiController]
[Authorize]
public class WebHookStripeController : ControllerBase
{
    private readonly IServiceManager _service;
    public WebHookStripeController(IServiceManager service)
    { 
_service=service;

        
    }








}