

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
[HttpPost("register")]
public async Task<IActionResult> SignInUser(UserForRegisterDto forRegisterDto)
{
    var result = await _service.AuthenticationService.RegisterUser(forRegisterDto);
    
    if (!result.Succeeded)
    {
        return BadRequest(result.Errors);
    }
 
    var loginDto = new UserLoginDto 
    { 
        UserName = forRegisterDto.UserName, 
        Password = forRegisterDto.Password 
    };
    
    var isValid = await _service.AuthenticationService.ValidateUser(loginDto);
    
    if (!isValid)
    {
        return Unauthorized();
    }
  
    var token = await _service.AuthenticationService.CreateToken();
    
    return Ok(new { Token = token });
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











