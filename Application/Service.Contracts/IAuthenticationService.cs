


using Core.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace  Service.Contracts;



public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegisterDto userForRegister);

   Task<bool> ValidateUser(UserLoginDto userLoginDto);
   Task<string> CreateToken();




}
