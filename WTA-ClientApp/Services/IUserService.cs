using WTA_ClientApp.Common;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> GetUserByIdAsync(string userId);
        Task<ServiceResult<ICollection<UserDto>>> GetAllUsersAsync();
        Task<ServiceResult<object>> UpdateUserAsync(UserDto dto);
        Task<ServiceResult<object>> CreateUserAsync(UserRegistrationDto dto);
    }
}
