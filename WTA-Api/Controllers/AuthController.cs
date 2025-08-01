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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var result = await accountService.LoginAsync(userLoginDto);
                if (result.Succeeded)
                {
                    return Ok(new { Message = "User logged in successfully." });
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
