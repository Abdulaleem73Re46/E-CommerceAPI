

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Core.Entities;
using Core.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Service.Contracts;

namespace Service;





public sealed class AuthenticationService : IAuthenticationService
{
     private readonly IMapper _mapper;
     private readonly IConfiguration _configuration;
     private readonly UserManager<User> _user;

  private User? user; // to read the claims from it  


     public AuthenticationService(IMapper mapper,UserManager<User> user,IConfiguration configuration)
    {
        
    _mapper=mapper;
    _user=user;
    _configuration=configuration;


    }
     


    public async Task<bool> ValidateUser(UserLoginDto userLoginDto)
    {      
        Console.WriteLine("at the start....");
          user=await _user.FindByNameAsync(userLoginDto.UserName);
           Console.WriteLine("after FindByName....");
          var result=await _user.CheckPasswordAsync(user,userLoginDto.Password);
         
        if (!result && user==null)
        {    Console.WriteLine("in the  if scope....");
            // to put the Logger messagge here
            return result;
        }
         Console.WriteLine("Out the if Scope....");
        return result;
    }

    public async Task<string> CreateToken()
    {
  var Credentials=GetSigningCredentials();
  var claims= await GetClaimsAsync();
  var tokenOptions=GenerateTokenOptions(Credentials,claims);
  
  var Jwt=new JwtSecurityTokenHandler().WriteToken(tokenOptions); //Responsible for Generate,Read,Check,Transform JWT Tokens 

   return Jwt;


       
    }

    public async Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegister)
    {
         var userEntity=_mapper.Map<User>(userForRegister);
         var createduserOrResult=await _user.CreateAsync(userEntity,userForRegister.Password);

        if (createduserOrResult.Succeeded && userForRegister.Roles!= null && userForRegister.Roles.Any())
        {
            await _user.AddToRolesAsync(userEntity,userForRegister.Roles);

        }
        else if (createduserOrResult.Succeeded)
        {
            await _user.AddToRoleAsync(userEntity,"User");
        }

  return createduserOrResult;


    }




    private SigningCredentials GetSigningCredentials()
    {
        var key=Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETKEY"));
        var secret=new SymmetricSecurityKey(key);
        return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
    }


    private async Task<List<Claim>> GetClaimsAsync()
    {
        var claims=new List<Claim>
        {
          new Claim(ClaimTypes.Name,user.UserName)  

        };
    
    var Roles=await _user.GetRolesAsync(user);

    foreach(var role in Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role));

        }

return claims;

    }

private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
     var jwtSettings=_configuration.GetSection("JwtSettings");
     var tokenOptions=new JwtSecurityToken
     (
         issuer:jwtSettings["Issuer"],
         audience:jwtSettings["Audience"],
         claims:claims,
         expires:DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireInMinutes"])),

         signingCredentials:signingCredentials
     );
     return tokenOptions;

    }



public async Task<UserDto> GetLoggedInUserAsync()
{
    if (user == null)
        throw new InvalidOperationException("No user is logged in. Call ValidateUser first.");
    
    var userDto = _mapper.Map<UserDto>(user);
    var roles = await _user.GetRolesAsync(user);
    userDto.Roles = roles.ToList();
    
    return userDto;
}


    public async Task<UserDto>  GetUserByUserNameAsync(string username)
    {
        var user=await _user.FindByNameAsync(username);
         if(user is null)throw new KeyNotFoundException($"User with UserName {username} not found in database");

        var userDto= _mapper.Map<UserDto>(user);
        return userDto;



    }

   

}   