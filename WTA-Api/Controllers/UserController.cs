using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
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
        public async Task<ActionResult<List<Employee>>> GetAllEmpoyees()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
