using Microsoft.EntityFrameworkCore;
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
    }
}
