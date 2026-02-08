using Microsoft.EntityFrameworkCore;
using stock_fincance_api.Data;
using stock_fincance_api.DTOs;
using stock_fincance_api.Models;
using stock_fincance_api.Repositoy;

namespace stock_fincance_api.Controllers.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

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

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);

        }

        public async Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto stockDto)
        {
            var existingstock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingstock == null)
            {
                return null;
            }
            existingstock.Symbol = stockDto.Symbol;
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
