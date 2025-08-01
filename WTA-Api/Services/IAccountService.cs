using Microsoft.AspNetCore.Identity;
using WTA_Api.DTOs;

namespace WTA_Api.Services
{
    public interface IAccountService
    {
        Task CreateUserFromDtoAsync(UserRegistrationDto userRegistrationDto);
        Task<SignInResult> LoginAsync(UserLoginDto userLoginDto);
    }
}
