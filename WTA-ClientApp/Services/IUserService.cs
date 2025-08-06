using WTA_ClientApp.Common;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> GetUserByIdAsync(string userId);
    }
}
