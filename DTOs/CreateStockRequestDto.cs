using stock_fincance_api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_fincance_api.DTOs
{
    public class CreateStockRequestDto
    {
    
        public string? Symbol { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }

  
        
    }
}


