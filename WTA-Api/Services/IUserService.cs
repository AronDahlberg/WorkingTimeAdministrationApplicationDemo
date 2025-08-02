using WTA_Api.DTOs;

namespace WTA_Api.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<List<UserDto>> GetAllEmployeesAsync();
        Task<bool> UpdateUserAsync(UserDto user, bool isAdmin);
        Task<UserDto?> GetUserByIdAsync(string userId);
    }
}
