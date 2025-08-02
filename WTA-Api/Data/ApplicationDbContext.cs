using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WTA_Api.Models;

namespace WTA_Api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApiUser>(options)
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<WorkEntry> WorkEntries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "bbaf5542-1f87-4f38-a293-ad48fba5bea1"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "6e378458-45af-4c71-b376-ad5b22a5f92e"
                }
            );

            builder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = -1,
                    FirstName = "Admin",
                    LastName = "User",
                    SocialSecurityNumber = "000000000000",
                    PhoneNumber = "123-456-7890",
                    EmergencyContactNumber = "098-765-4321",
                    Country = String.Empty,
                    City = String.Empty,
                    Address = String.Empty,
                    PostalCode = "12345"
                },
                new Employee
                {
                    EmployeeId = -2,
                    FirstName = "Test",
                    LastName = "User",
                    SocialSecurityNumber = "111111111111",
                    PhoneNumber = "123-456-7890",
                    EmergencyContactNumber = "098-765-4321",
                    Country = String.Empty,
                    City = String.Empty,
                    Address = String.Empty,
                    PostalCode = "12345"
                }
            );

            builder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "989ac103-e470-4287-b345-c6bbc77bbdd5",
                    Email = "admin@wta.com",
                    NormalizedEmail = "ADMIN@WTA.COM",
                    UserName = "admin@wta.com",
                    NormalizedUserName = "ADMIN@WTA.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApiUser>().HashPassword(null, "Admin123!"),
                    EmployeeId = -1,
                },
                new ApiUser
                {
                    Id = "5da9e3d2-c25a-4c00-b352-e5b20a15fb88",
                    Email = "test.user@wta.com",
                    NormalizedEmail = "TEST.USER@WTA.COM",
                    UserName = "test.user@wta.com",
                    NormalizedUserName = "TEST.USER@WTA.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApiUser>().HashPassword(null, "User123!"),
                    EmployeeId = -2,
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "989ac103-e470-4287-b345-c6bbc77bbdd5",
                    RoleId = "6e378458-45af-4c71-b376-ad5b22a5f92e"
                },
                new IdentityUserRole<string>
                {
                    UserId = "5da9e3d2-c25a-4c00-b352-e5b20a15fb88",
                    RoleId = "bbaf5542-1f87-4f38-a293-ad48fba5bea1"
                }
            );
        }
    }
}
