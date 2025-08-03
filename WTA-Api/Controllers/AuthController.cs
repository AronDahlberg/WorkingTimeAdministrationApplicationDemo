using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WTA_Api.DTOs;
using WTA_Api.Models;
using WTA_Api.Services;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAccountService accountService) : Controller
    {
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="userRegistrationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                await accountService.CreateUserFromDtoAsync(userRegistrationDto);

                return Ok(new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while registering the user.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Logs in a user with the provided login credentials and returns an authentication response.
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>
        /// Returns an AuthResponse containing the user's authentication token and other details if successful, or an Unauthorized response if login fails.
        /// </returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserLoginDto userLoginDto)
        {
            try
            {
                AuthResponse? response = await accountService.LoginAsync(userLoginDto);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid login attempt." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while logging in.", Details = ex.Message });
            }
        }
    }
}
