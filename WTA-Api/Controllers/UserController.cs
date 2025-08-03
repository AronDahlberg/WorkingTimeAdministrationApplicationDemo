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

        /// <summary>
        /// Retrieves user data for the specified user ID.
        /// Admin users can retrieve any user's data, while non-admin users can only retrieve their own data.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Returns the user data as a UserDto if found, or an Unauthorized response if the user is not authorized to access the data.
        /// </returns>
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

        /// <summary>
        /// Updates user data for the specified user.
        /// Admin users can update any user's data, while non-admin users can only update their own data.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves a list of all employees in the system.
        /// Only accessible by admin users.
        /// Will include all employees, even those without a user account.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves a list of all users in the system.
        /// Only accessible by admin users.
        /// Will not include any user-less employees (e.g., those without a user account).
        /// </summary>
        /// <returns></returns>
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
