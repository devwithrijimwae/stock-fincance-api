using stock_fincance_api.Data;
using stock_fincance_api.Models;
using stock_fincance_api.Repositoy;
using System.Data.Entity;

namespace stock_fincance_api.Controllers.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
        }

        public Task<Stock> CreateAsync(Stock stockModel)
        {
            _context.Stocks.AddAsync(stockModel);
            _context.SaveChangesAsync();
            return stockModel;
        }
    }
}
