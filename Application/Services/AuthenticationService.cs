using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;

namespace Service;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;  
    private User? _currentUser;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager; 
        _configuration = configuration;
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

    // public async Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegister)
    // {
    //     var userEntity = _mapper.Map<User>(userForRegister);

    //     var result = await _userManager.CreateAsync(userEntity, userForRegister.Password);

    //     if (result.Succeeded && userForRegister.Roles != null && userForRegister.Roles.Any())
    //     {
    //         await _userManager.AddToRolesAsync(userEntity, userForRegister.Roles);
    //     }
    //     else if (result.Succeeded)
    //     {
    //         await _userManager.AddToRoleAsync(userEntity, "User");
    //     }

    //     return result;
    // }
    


    private SigningCredentials GetSigningCredentials()
    {
        //var secretKey = Environment.GetEnvironmentVariable("SECRETKEY"); 
          //var secretKey = "YourSuperSecretKeyThatIsAtLeast32CharactersLong123!";
          var section=_configuration.GetSection("JwtSettings");
          var secretKey=section["Key"];
          

        // if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
        // {
        //     throw new InvalidOperationException("JWT Secret Key is missing or too short. Please provide a key with at least 32 characters.");
        // }
        
        var key = Encoding.UTF8.GetBytes(secretKey);
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
        var jwtSettings = _configuration.GetSection("JwtSettings");
        
    
        var issuer = jwtSettings["Issuer"] ;
        var audience = jwtSettings["Audience"];
        var expireInMinutes = Convert.ToDouble(jwtSettings["ExpireInMinutes"]);
        
        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
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



        var token=await CreateTokenAsync(userEntity);
        return new AuthResponse
        {
            Succeeded=true,
            Token=token
            
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

public async Task<AuthResponse> LoginAsync(UserLoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return new AuthResponse { Succeeded = false, Errors = new[] { "Invalid username or password" } };

        // إزالة Refresh Tokens المنتهية أو الملغاة
        user.RefreshTokens.RemoveAll(rt => !rt.IsActive);
        var refreshToken = _tokenService.CreateRefreshToken();
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = DateTime.UtcNow.AddDays(7)
        });   
        await _userManager.UpdateAsync(user);

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.CreateAccessToken(user, roles);

        return new AuthResponse
        {
            Succeeded = true,
            Token = accessToken,
            RefreshToken = refreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpireInMinutes"]))
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        // استخراج الـ Claims من الـ Access Token المنتهي
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return new AuthResponse { Succeeded = false, Errors = new[] { "Invalid access token" } };

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new AuthResponse { Succeeded = false, Errors = new[] { "User not found" } };

        var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken && rt.IsActive);
        if (refreshToken == null)
            return new AuthResponse { Succeeded = false, Errors = new[] { "Invalid or expired refresh token" } };

        // إلغاء الـ Refresh Token المستخدم وإنشاء جديد
        refreshToken.RevokedOn = DateTime.UtcNow;
        var newRefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = DateTime.UtcNow.AddDays(7)
        });
        await _userManager.UpdateAsync(user);

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = _tokenService.CreateAccessToken(user, roles);

        return new AuthResponse
        {
            Succeeded = true,
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpireInMinutes"]))
        };
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Key"];
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = false  // لا تتحقق من انتهاء الصلاحية
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

}






// Key
// Issuer
// Audience
// ExpireInMinutes








