using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    private readonly UserManager<User> _userManager;  // ✅ تغيير الاسم ليكون واضحاً
    private User? _currentUser;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;  // ✅ UserManager
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
        var claims = await GetClaimsAsync();
        var tokenOptions = GenerateTokenOptions(credentials, claims);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegister)
    {
        var userEntity = _mapper.Map<User>(userForRegister);
        var result = await _userManager.CreateAsync(userEntity, userForRegister.Password);

        if (result.Succeeded && userForRegister.Roles != null && userForRegister.Roles.Any())
        {
            await _userManager.AddToRolesAsync(userEntity, userForRegister.Roles);
        }
        else if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(userEntity, "User");
        }

        return result;
    }

    private SigningCredentials GetSigningCredentials()
    {
        // ✅ محاولة قراءة المفتاح من عدة مصادر
        var secretKey = _configuration["JwtSettings:Key"] ??      // من appsettings.json
                       _configuration["Jwt:Key"] ??               // من Jwt:Key
                       Environment.GetEnvironmentVariable("SECRETKEY") ??  // من Environment Variables
                       "YourSuperSecretKeyThatIsAtLeast32CharactersLong123!"; // ✅ fallback آمن للتطوير
        
        if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
        {
            throw new InvalidOperationException("JWT Secret Key is missing or too short. Please provide a key with at least 32 characters.");
        }
        
        var key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaimsAsync()
    {
        if (_currentUser == null)
            throw new InvalidOperationException("User is not initialized. Call ValidateUser() first.");
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, _currentUser.Id),
            new Claim(ClaimTypes.Name, _currentUser.UserName ?? ""),
            new Claim(ClaimTypes.Email, _currentUser.Email ?? "")
        };
        
        var roles = await _userManager.GetRolesAsync(_currentUser);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        
        // ✅ قيم افتراضية إذا كانت الإعدادات غير موجودة
        var issuer = jwtSettings["Issuer"] ?? "https://localhost:5276";
        var audience = jwtSettings["Audience"] ?? "https://localhost:5276";
        var expireInMinutes = Convert.ToDouble(jwtSettings["ExpireInMinutes"] ?? "60");
        
        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expireInMinutes),
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
}