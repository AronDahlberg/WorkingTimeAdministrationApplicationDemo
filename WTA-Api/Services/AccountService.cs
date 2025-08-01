using Microsoft.AspNetCore.Identity;
using WTA_Api.Data;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Services
{
    public class AccountService(UserManager<ApiUser> userManager, IEmployeeRepository employeeRepository)
    {
        private readonly UserManager<ApiUser> userManager = userManager;
        private readonly IEmployeeRepository employeeRepository = employeeRepository;

        public async Task CreateUserFromDtoAsync(UserRegistrationDto userRegistrationDto)
        {
            var existingUser = await userManager.FindByEmailAsync(userRegistrationDto.Email);

            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            var employee = await employeeRepository.GetEmployeeBySSNAsync(userRegistrationDto.SocialSecurityNumber);

            if (employee == null)
            {
                employee = new()
                {
                    FirstName = userRegistrationDto.FirstName,
                    LastName = userRegistrationDto.LastName,
                    SocialSecurityNumber = userRegistrationDto.SocialSecurityNumber,
                    PhoneNumber = userRegistrationDto.PhoneNumber,
                    EmergencyContactNumber = userRegistrationDto.EmergencyContactNumber,
                    Country = userRegistrationDto.Country,
                    City = userRegistrationDto.City,
                    Address = userRegistrationDto.Address,
                    PostalCode = userRegistrationDto.PostalCode
                };

                try
                {
                    await employeeRepository.AddEmployeeAsync(employee);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while creating the employee record.", ex);
                } 
            }

            ApiUser user = new()
            {
                UserName = userRegistrationDto.Email,
                Email = userRegistrationDto.Email,
                Employee = employee
            };

            IdentityResult result = await userManager.CreateAsync(user, userRegistrationDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("An error occurred while creating the user account: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(user, "User");
        }

        public async Task<SignInResult> LoginAsync(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                throw new ArgumentNullException(nameof(userLoginDto), "User login data cannot be null.");
            }
            var user = await userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            var result = await userManager.CheckPasswordAsync(user, userLoginDto.Password);
            return result ? SignInResult.Success : SignInResult.Failed;
        }
    }
}
