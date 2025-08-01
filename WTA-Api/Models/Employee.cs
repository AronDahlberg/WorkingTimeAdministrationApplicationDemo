using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WTA_Api.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "SSN must be exactly 12 digits.")]
        public string SocialSecurityNumber { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string EmergencyContactNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;


        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;


        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;


        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Postal code must be numeric and between 4 and 10 digits.")]
        public string PostalCode { get; set; } = string.Empty;

        [Range(0, 1000, ErrorMessage = "Hourly wage must be between 0 and 1000.")]
        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal HourlyWage { get; set; }
    }
}
