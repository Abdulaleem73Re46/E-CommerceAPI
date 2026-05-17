

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
          user=await _user.FindByNameAsync(userLoginDto.UserName);
         var result=(user !=null && await _user.CheckPasswordAsync(user,userLoginDto.Password));
        if (!result)
        {
            // to put the Logger messagge here
            return result;
        }
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
         var createduserOrResult=await _user.CreateAsync(userEntity,userEntity.PasswordHash);

        if (createduserOrResult.Succeeded)
        {
            await _user.AddToRolesAsync(userEntity,userForRegister.Roles);

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






}   