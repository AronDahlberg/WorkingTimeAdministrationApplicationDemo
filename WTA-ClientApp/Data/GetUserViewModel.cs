using System.ComponentModel.DataAnnotations;

namespace WTA_ClientApp.Data
{
    public class GetUserViewModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
