using stock_fincance_api.DTOs.Stocks;
using stock_fincance_api.Models;

namespace stock_fincance_api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                lastDiv = stockModel.lastDiv,
                MarketCap = stockModel.MarketCap,

            };
        }
    }
}
