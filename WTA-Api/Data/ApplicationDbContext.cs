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
        }
    }
}
