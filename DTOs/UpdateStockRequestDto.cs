using System.ComponentModel.DataAnnotations;

namespace stock_fincance_api.DTOs
{
    public class UpdateStockRequestDto
    {

        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 character")]
        public string? Symbol { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Company Name cannot be over 10 character")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry Name cannot be over 10 character")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }

    }
}
