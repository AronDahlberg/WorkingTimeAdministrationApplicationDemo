using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WTA_Api.DTOs;
using WTA_Api.Models;
using WTA_Api.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : Controller
    {
        private readonly IUserService userService = userService;

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployeeData(int employeeId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("UpdateEmployee")]
        public async Task<ActionResult<Employee>> UpdateEmployeeData(Employee employee)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<UserDto>> GetUserData(string userId)
        {
            var tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (tokenUserId == null)
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && userId != tokenUserId)
                return Forbid();

            var user = await userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUserData(UserDto user)
        {
            try
            {
                var tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (tokenUserId == null)
                    return Unauthorized();

                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin && user.UserId != tokenUserId)
                    return Forbid();

                var updateresult = await userService.UpdateUserAsync(user, isAdmin);

                if (!updateresult)
                {
                    return BadRequest(new { Message = "Failed to update user data." });
                }

                return Ok(new { Message = "User data updated successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating user data.", Details = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDto>>> GetAllEmployees()
        {
            try
            {
                var employees = await userService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving employees." });
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving users." });
            }
        }
    }
}
