using Microsoft.EntityFrameworkCore;
using stock_fincance_api.Data;
using stock_fincance_api.DTOs;
using stock_fincance_api.Helper;
using stock_fincance_api.Models;
using stock_fincance_api.Repositoy;

namespace stock_fincance_api.Controllers.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        private int skipNumber;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?>DeleteAsync(int Id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                 stocks = stocks.Where(s => s.symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.symbol) : stocks.OrderBy(s => s.symbol);
                }
                var skip = (query.PageNumber - 1) * query.PageSize;
            }
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id==id);

        }

        public Task<bool> StockExists(int Id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == Id);
        }
        public async Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto stockDto)
        {
            var existingstock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingstock == null)
            {
                return null;
            }
            existingstock.symbol = stockDto.Symbol;
            existingstock.CompanyName = stockDto.CompanyName;
            existingstock.Purchase = stockDto.Purchase;
            existingstock.LastDiv = stockDto.LastDiv;
            existingstock.Industry = stockDto.Industry;
            existingstock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingstock;
        }

    
        
    }
}
