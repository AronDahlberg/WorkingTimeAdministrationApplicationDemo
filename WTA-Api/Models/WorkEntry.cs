using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTA_Api.Models
{
    public class WorkEntry
    {
        [Key]
        public int WorkEntryId { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00:00", "23:59:59", ErrorMessage = "Duration must be less than 24 hours.")]
        public TimeSpan Duration { get; set; }

        [Range(0, 24000, ErrorMessage = "Total wage must be between 0 and 24000.")]
        [DataType(DataType.Currency)]
        public decimal TotalWage { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
    }
}
