

using Core.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ECommerce.Presentation;



[Route("api/users")]
[ApiController]

public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;
public AuthenticationController(IServiceManager service)
{
    _service=service;
    
}


[HttpPost("signin")]
public async Task<IActionResult> SignInUser([FromBody] UserForRegisterDto forRegisterDto)
    {
      var createduser=await _service.AuthenticationService.RegisterUser(forRegisterDto);

        if (!createduser.Succeeded)
        {
            foreach(var error in createduser.Errors)
            {
                ModelState.TryAddModelError(error.Code,error.Description);
            }
            return BadRequest(ModelState);
        }
return StatusCode(201);
    }


    
    [HttpPost("login")]
    public async Task<IActionResult> LogInUser([FromBody] UserLoginDto userLoginDto)
    {
        if(!await _service.AuthenticationService.ValidateUser(userLoginDto))
        {
            return Unauthorized();
        }
   
   var token=await _service.AuthenticationService.CreateToken();
   return Ok(new {Token=token});


    }







}











