using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WTA_Api.DTOs
{
    public class AddWorkEntryDto
    {
        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00:00", "23:59:59", ErrorMessage = "Duration must be less than 24 hours.")]
        public TimeSpan Duration { get; set; }

        [Range(0, 24000, ErrorMessage = "Total wage must be between 0 and 24000.")]
        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal TotalWage { get; set; }

        [Required]
        public int EmployeeId { get; set; }
    }
}
