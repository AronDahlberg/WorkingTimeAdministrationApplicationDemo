using Microsoft.AspNetCore.Identity;

namespace WTA_Api.Models
{
    public class ApiUser : IdentityUser
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
