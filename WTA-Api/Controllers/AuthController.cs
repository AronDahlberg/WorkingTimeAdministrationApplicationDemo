using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApiUser> userManager) : Controller
    {
        private readonly UserManager<ApiUser> userManager = userManager;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                ApiUser user = accountManager.CreateUserFromDto(userRegistrationDto);

                IdentityResult result = await userManager.CreateAsync(user, userRegistrationDto.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");

                    return Ok(new { Message = "User registered successfully." });
                }
                else
                {
                    return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while registering the user.", Details = ex.Message });
            }
        }
    }
}
