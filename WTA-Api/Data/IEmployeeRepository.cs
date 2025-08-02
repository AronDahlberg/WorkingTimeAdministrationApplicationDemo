using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Data
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task<Employee?> GetEmployeeBySSNAsync(string socialSecurityNumber);
        Task<List<UserDto>> GetAllEmployeesWithUsersAsync();
    }
}
