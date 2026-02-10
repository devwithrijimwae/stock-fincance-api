using System.ComponentModel.DataAnnotations;

namespace stock_fincance_api.Models
{
    public class Register
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Pasword { get; set; }
    }
}
