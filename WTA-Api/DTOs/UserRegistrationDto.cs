using System.ComponentModel.DataAnnotations;

namespace WTA_Api.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long and no more than 100 characters.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
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
        [StringLength(100, ErrorMessage = "Country cannot be longer than 100 characters.")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters.")]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(20, ErrorMessage = "Postal code must be between 4 and 10 digits.")]
        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Postal code must be numeric and between 4 and 10 digits.")]
        public string PostalCode { get; set; } = string.Empty;
    }
}
