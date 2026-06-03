using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;

namespace Service;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;

    private readonly UserManager<User> _userManager;  
    private User? _currentUser;
    private readonly JWTSettings _JwtSettings;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager,IOptions<JWTSettings> JwtSettings)
    {
        _mapper = mapper;
        _userManager = userManager; 
    
        _JwtSettings=JwtSettings.Value;

    }

    public async Task<bool> ValidateUser(UserLoginDto userLoginDto)
    {
        Console.WriteLine("Validating user...");
        _currentUser = await _userManager.FindByNameAsync(userLoginDto.UserName);
        
        if (_currentUser == null)
        {
            Console.WriteLine("User not found");
            return false;
        }
    
        var result = await _userManager.CheckPasswordAsync(_currentUser, userLoginDto.Password);

        Console.WriteLine($"Password check result: {result}");
        return result;
    }

    public async Task<string> CreateToken()
    {
        if (_currentUser == null)
            throw new InvalidOperationException("User must be validated first. Call ValidateUser() before CreateToken().");
        
        var credentials = GetSigningCredentials();
        var claims = await GetClaimsAsync(_currentUser);
        var tokenOptions = GenerateTokenOptions(credentials, claims);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

 


    private SigningCredentials GetSigningCredentials()
    {
        //var secretKey = Environment.GetEnvironmentVariable("SECRETKEY"); 
          //var secretKey = "YourSuperSecretKeyThatIsAtLeast32CharactersLong123!";
         // var section=   ;//_configuration.GetSection("JwtSettings");
        //   var secretKey=_JwtSettings.Key;
          
        // if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
        // {
        //     throw new InvalidOperationException("JWT Secret Key is missing or too short. Please provide a key with at least 32 characters.");
        // }
        
        var key = Encoding.UTF8.GetBytes(_JwtSettings.Key);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }


    private async Task<List<Claim>> GetClaimsAsync(User user)
    {
        // if (user == null)
        //     throw new InvalidOperationException("User is not initialized. Call ValidateUser first.");
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email,user.Email)
         
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
       
     
        var expireInMinutes = Convert.ToDouble(_JwtSettings.ExpireInMinutes);
        
        var tokenOptions = new JwtSecurityToken(
            issuer: _JwtSettings.Issuer,
            audience: _JwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireInMinutes),
            signingCredentials: signingCredentials
        );
        
        return tokenOptions;
    }

    public async Task<UserDto> GetLoggedInUserAsync()
    {
        if (_currentUser == null)
            throw new InvalidOperationException("No user is logged in. Call ValidateUser first.");
        
        var userDto = _mapper.Map<UserDto>(_currentUser);
        var roles = await _userManager.GetRolesAsync(_currentUser);
        userDto.Roles = roles.ToList();
        
        return userDto;
    }

    public async Task<UserDto> GetUserByUserNameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            throw new KeyNotFoundException($"User with UserName {username} not found in database");

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<AuthResponse> ResgisterUserAsync(UserForRegisterDto userForRegisterDto)
    {      
           var userEntity=_mapper.Map<User>(userForRegisterDto);
           var result=await _userManager.CreateAsync(userEntity,userForRegisterDto.Password);
        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Succeeded=false,
                Errors=result.Errors.Select(e=>e.Description)
            };}
        if (result.Succeeded && userForRegisterDto.Roles != null && userForRegisterDto.Roles.Any())
        {
            await _userManager.AddToRolesAsync(userEntity, userForRegisterDto.Roles);
        }
        // if (result.Succeeded)
        // {
        //       await _userManager.AddToRoleAsync(userEntity, "user");
        // }
        var refreshtoken=CreateRefreshToken();
        userEntity.RefreshTokens.Add(new RefreshToken
        {
            Token=refreshtoken,
            ExpiresOn=DateTime.UtcNow.AddMinutes(10)
            
        });

await _userManager.UpdateAsync(userEntity);


        var token=await CreateTokenAsync(userEntity);
        return new AuthResponse
        {
            Succeeded=true,
            Token=token,
            RefreshToken=refreshtoken,
            Expiration=DateTime.UtcNow.AddMinutes(Convert.ToDouble(_JwtSettings.ExpireInMinutes))
            
        };   
            
    }

public async Task<string>  CreateTokenAsync(User user)
    {
         if (user == null)
            throw new InvalidOperationException("User must be validated first. Call ValidateUser() before CreateToken().");
        
        var credentials = GetSigningCredentials();
        var claims = await GetClaimsAsync(user);
        var tokenOptions = GenerateTokenOptions(credentials, claims);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);


    }

 private string CreateRefreshToken()
    {
        var randomNumber=new byte[32];
        using var rng=RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);

    }
    


    public async Task<AuthResponse>     LoginAsync(UserLoginDto userLoginDto){
        var user=await _userManager.FindByNameAsync(userLoginDto.UserName);
        if(user==null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return new AuthResponse{Succeeded=false,Errors=new [] {"Invalid UserName Or Password"}};

        }
        // إزالة Refresh Tokens المنتهية أو الملغاة
        user.RefreshTokens.RemoveAll(rt => !rt.IsActive);
        var refreshToken =CreateRefreshToken();
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = DateTime.UtcNow.AddMinutes(3)
        });   
        await _userManager.UpdateAsync(user);

       
        var accessToken =await CreateTokenAsync(user);

        return new AuthResponse
        {
            Succeeded = true,
            Token = accessToken,
            RefreshToken = refreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_JwtSettings.ExpireInMinutes))
        };

    }

 



private async Task<User?> GetUserByAccessTokenAsync(string accessToken)
    {
        var principal=GetPrincipalFromExpiredToken(accessToken);
        if(principal==null)return null;
        var userId=principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _userManager.FindByIdAsync(userId);



    }



    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        // var jwtSettings = _configuration.GetSection("JwtSettings");
        // var secretKey = jwtSettings["Key"];
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSettings.Key)),
            ValidateLifetime = false  
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;
            return principal;
        }
        catch
        {
            return null;  
        }
    }


public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        
var user=await GetUserByAccessTokenAsync(refreshTokenRequestDto.AccessToken);
if(user==null)return new AuthResponse{Succeeded=false,Errors= new[]{"Invalid access token "}};
var storedRefreshToken=user.RefreshTokens.FirstOrDefault(rt=>rt.Token==refreshTokenRequestDto.RefreshToken && rt.IsActive);
if(storedRefreshToken==null) return new AuthResponse{Succeeded=false,Errors=new []{"Invalid or expired refresh token"}};


storedRefreshToken.RevokedOn=DateTime.UtcNow;
var newRefreshToken=CreateRefreshToken();
user.RefreshTokens.Add(new RefreshToken
{
   Token=newRefreshToken,
   ExpiresOn=DateTime.UtcNow.AddMinutes(3) 

});

await _userManager.UpdateAsync(user);
var newAccessToken=await CreateTokenAsync(user);

return new AuthResponse
{
    Succeeded=true,
    Token=newAccessToken,
    RefreshToken=newRefreshToken,
    Expiration=DateTime.UtcNow.AddMinutes(Convert.ToDouble(_JwtSettings.ExpireInMinutes))
    
};




    }



}














