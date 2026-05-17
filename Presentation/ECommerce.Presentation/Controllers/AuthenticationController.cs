

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
[ServiceFilter(typeof(ValidationFilterAttribute))]
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
          var token=await _service.AuthenticationService.CreateToken();
return Ok(new {Token =token});
    }


    
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> LogInUser([FromBody] UserLoginDto userLoginDto)
    {
        if(!await _service.AuthenticationService.ValidateUser(userLoginDto))
        {  Console.WriteLine($"this is the {userLoginDto.UserName} and {userLoginDto.Password}");
            return Unauthorized();
        }
   
   var token=await _service.AuthenticationService.CreateToken();
 var user=await _service.AuthenticationService.GetUserByUserNameAsync(userLoginDto.UserName);




   return Ok(new {Token=token,User=user});


    }







}











