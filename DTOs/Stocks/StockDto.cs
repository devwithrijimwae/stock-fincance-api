using stock_fincance_api.DTOs.Comments;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_fincance_api.DTOs.Stocks
{
    public class StockDto
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal lastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public required List<CommentDto> Comments { get; set; }

    }

}
