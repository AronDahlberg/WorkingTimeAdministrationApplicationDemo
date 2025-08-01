using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTA_Api.Migrations
{
    /// <inheritdoc />
    public partial class bugfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "989ac103-e470-4287-b345-c6bbc77bbdd5",
                columns: new[] { "ConcurrencyStamp", "EmployeeId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5ef91d2-cf1c-4ed7-94e6-b1a72cbc225b", -1, "AQAAAAIAAYagAAAAEP07skfXJF6DcH7IoQs4I8rkf0rk5HmQY4fiNERxuS5IV8+9X0XvpFtriSrOExeRGw==", "22565702-4fa4-48e7-b1f4-8b0b5a753d77" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "989ac103-e470-4287-b345-c6bbc77bbdd5",
                columns: new[] { "ConcurrencyStamp", "EmployeeId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b791d38f-d900-4209-8858-f64bd13500b0", 0, "AQAAAAIAAYagAAAAEOVyX05zBDAIJYz9Hi148RJNJlpfy5qu391EDSY4AcAWUwQQIzHSXr8K6Ig86wpoiA==", "aa3bcace-1a59-42b6-9b35-adf1f70adc80" });
        }
    }
}
