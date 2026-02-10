using System.ComponentModel.DataAnnotations;

namespace stock_fincance_api.DTOs
{
    public class LoginDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string Email { get; internal set; }
    }
}
