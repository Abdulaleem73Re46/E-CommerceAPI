


using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Helpers;
using Microsoft.AspNetCore.Identity;

namespace  Service.Contracts;



public interface IAuthenticationService
{
    //Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegister);

   Task<bool> ValidateUser(UserLoginDto userLoginDto);
   Task<string> CreateToken();
    Task<string> CreateTokenAsync(User user);
  Task<UserDto> GetLoggedInUserAsync();
   
   Task<UserDto> GetUserByUserNameAsync(string username);

Task<AuthResponse> ResgisterUserAsync(UserForRegisterDto userForRegisterDto);

Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
  Task<AuthResponse> LoginAsync(UserLoginDto userLoginDto);


}
