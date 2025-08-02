using Microsoft.EntityFrameworkCore;
using WTA_Api.DTOs;
using WTA_Api.Models;

namespace WTA_Api.Data
{
    public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task AddEmployeeAsync(Employee employee)
        {

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            }
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await context.Employees
                             .AsNoTracking()
                             .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {

            if (employeeId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(employeeId), "Employee ID must be greater than zero.");
            }
            return await context.Employees
                                .AsNoTracking()
                                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employee?> GetEmployeeBySSNAsync(string socialSecurityNumber)
        {

            if (string.IsNullOrWhiteSpace(socialSecurityNumber))
            {
                throw new ArgumentException("Social Security Number cannot be null or empty.", nameof(socialSecurityNumber));
            }
            return await context.Employees
                                .AsNoTracking()
                                .FirstOrDefaultAsync(e => e.SocialSecurityNumber == socialSecurityNumber);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            }
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllEmployeesWithUsersAsync()
        {
            return await (
                from e in context.Employees
                join u in context.Users
                  on e.EmployeeId equals u.EmployeeId into usersGroup
                from u in usersGroup.DefaultIfEmpty()
                select new UserDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    SocialSecurityNumber = e.SocialSecurityNumber,
                    PhoneNumber = e.PhoneNumber,
                    EmergencyContactNumber = e.EmergencyContactNumber,
                    Country = e.Country,
                    City = e.City,
                    Address = e.Address,
                    PostalCode = e.PostalCode,
                    HourlyWage = e.HourlyWage,

                    // if there’s no user, default to empty string
                    UserId = u != null ? u.Id : string.Empty,
                    Email = u != null ? (u.Email ?? string.Empty) : string.Empty
                }
            ).ToListAsync();
        }
    }
}
