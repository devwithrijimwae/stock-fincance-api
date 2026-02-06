using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using stock_fincance_api.Data;
using stock_fincance_api.DTOs;
using stock_fincance_api.Models;
using stock_fincance_api.Repositoy;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace stock_fincance_api.Controllers.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;

        public StockRepository(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        public async Task<List<Stock>> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> DeleteAsync(int Id)
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

        public async Task<List<Stock>> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Where(s => s.Id == id).ToListAsync();
            return stock;
        }

        public async Task<Stock> UpdateAsync(int Id, UpdateStockRequestDto stockDto)
        {
            var existingstock = _context.Stocks.FirstOrDefault(s => s.Id == Id);

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
