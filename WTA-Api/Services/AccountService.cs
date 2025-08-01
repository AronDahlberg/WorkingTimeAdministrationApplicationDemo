using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WTA_Api.Data;
using WTA_Api.DTOs;
using WTA_Api.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WTA_Api.Services
{
    public class AccountService(UserManager<ApiUser> userManager, IConfiguration configuration,  IEmployeeRepository employeeRepository) : IAccountService
    {
        private readonly UserManager<ApiUser> userManager = userManager;
        private readonly IEmployeeRepository employeeRepository = employeeRepository;
        private readonly IConfiguration configuration = configuration;

        public async Task CreateUserFromDtoAsync(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null)
            {
                throw new ArgumentNullException(nameof(userRegistrationDto), "User registration data cannot be null.");
            }

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

        public async Task<AuthResponse?> LoginAsync(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                throw new ArgumentNullException(nameof(userLoginDto), "User login data cannot be null.");
            }
            var user = await userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
            {
                return null;
            }
            var result = await userManager.CheckPasswordAsync(user, userLoginDto.Password);

            var tokenString = await GenerateToken(user);

            var response = new AuthResponse
            {
                Email = user.Email,
                Token = tokenString,
                UserId = user.Id
            };

            return response;
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? throw new Exception("Could not find secrete key to genereate token")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("EmployeeId", user.EmployeeId.ToString())
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
