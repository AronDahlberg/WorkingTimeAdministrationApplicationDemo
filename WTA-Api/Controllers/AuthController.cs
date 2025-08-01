using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WTA_Api.DTOs;
using WTA_Api.Models;
using WTA_Api.Services;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(AccountService accountService) : Controller
    {
        private readonly AccountService accountService = accountService;

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
    }
}
