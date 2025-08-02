using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WTA_Api.Data;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public class UserService(IEmployeeRepository employeeRepository, UserManager<ApiUser> userManager) : IUserService
    {
        private readonly IEmployeeRepository employeeRepository = employeeRepository;
        private readonly UserManager<ApiUser> userManager = userManager;

        public Task<List<UserDto>> GetAllEmployeesAsync()
        {
            return employeeRepository.GetAllEmployeesWithUsersAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await userManager.Users
                .Include(u => u.Employee)
                .ToListAsync();

            var dtos = users
            .Select(u => new UserDto
            {
                UserId = u.Id,
                Email = u.Email ?? throw new Exception("An account does not have an email in the database"),
                EmployeeId = u.Employee.EmployeeId,
                FirstName = u.Employee.FirstName,
                LastName = u.Employee.LastName,
                SocialSecurityNumber = u.Employee.SocialSecurityNumber,
                PhoneNumber = u.Employee.PhoneNumber,
                EmergencyContactNumber = u.Employee.EmergencyContactNumber,
                Country = u.Employee.Country,
                City = u.Employee.City,
                Address = u.Employee.Address,
                PostalCode = u.Employee.PostalCode,
                HourlyWage = u.Employee.HourlyWage
            })
            .ToList();

            return dtos;
        }

        public Task<bool> UpdateUserAsync(UserDto user, bool isAdmin)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var apiUser = userManager.Users.FirstOrDefault(u => u.Id == user.UserId) ?? throw new KeyNotFoundException($"User with ID {user.UserId} not found.");

            if (!isAdmin && apiUser.Employee.HourlyWage != user.HourlyWage)
            {
                throw new UnauthorizedAccessException("Only admins can change the hourly wage.");
            }

            apiUser.Email = user.Email;
            apiUser.Employee.FirstName = user.FirstName;
            apiUser.Employee.LastName = user.LastName;
            apiUser.Employee.SocialSecurityNumber = user.SocialSecurityNumber;
            apiUser.Employee.PhoneNumber = user.PhoneNumber;
            apiUser.Employee.EmergencyContactNumber = user.EmergencyContactNumber;
            apiUser.Employee.Country = user.Country;
            apiUser.Employee.City = user.City;
            apiUser.Employee.Address = user.Address;
            apiUser.Employee.PostalCode = user.PostalCode;
            apiUser.Employee.HourlyWage = user.HourlyWage;

            var result = userManager.UpdateAsync(apiUser).Result;

            return Task.FromResult(result.Succeeded);
        }
    }
}
