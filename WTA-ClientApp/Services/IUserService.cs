using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(string userId);
    }
}
