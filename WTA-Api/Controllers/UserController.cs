using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTA_Api.DTOs;
using WTA_Api.Models;
using WTA_Api.Services;

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
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUserData(UserDto user)
        {
            throw new NotImplementedException();
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
