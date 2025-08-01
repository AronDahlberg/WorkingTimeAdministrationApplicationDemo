using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTA_Api.Migrations
{
    /// <inheritdoc />
    public partial class addedSeededAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "City", "Country", "EmergencyContactNumber", "FirstName", "HourlyWage", "LastName", "PhoneNumber", "PostalCode", "SocialSecurityNumber" },
                values: new object[] { -1, "", "", "", "098-765-4321", "Admin", 0m, "User", "123-456-7890", "12345", "000000000000" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EmployeeId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "989ac103-e470-4287-b345-c6bbc77bbdd5", 0, "b791d38f-d900-4209-8858-f64bd13500b0", "admin@wta.com", true, 0, false, null, "ADMIN@WTA.COM", "ADMIN@WTA.COM", "AQAAAAIAAYagAAAAEOVyX05zBDAIJYz9Hi148RJNJlpfy5qu391EDSY4AcAWUwQQIzHSXr8K6Ig86wpoiA==", null, false, "aa3bcace-1a59-42b6-9b35-adf1f70adc80", false, "admin@wta.com" });
            
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6e378458-45af-4c71-b376-ad5b22a5f92e", "989ac103-e470-4287-b345-c6bbc77bbdd5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6e378458-45af-4c71-b376-ad5b22a5f92e", "989ac103-e470-4287-b345-c6bbc77bbdd5" });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "989ac103-e470-4287-b345-c6bbc77bbdd5");
        }
    }
}
