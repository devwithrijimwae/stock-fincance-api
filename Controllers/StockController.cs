using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stock_fincance_api.Data;
using stock_fincance_api.DTOs;
using stock_fincance_api.Mappers;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace stock_fincance_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
                var stockDto = stocks.Select(s => StockMapper.ToStockDto(s));

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return base.Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel1 = stockDto.ToStockFromCreateDto();
           await _context.Stocks.AddAsync(stockModel1);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel1.Id }, stockModel1.ToStockDto());

        }
        [HttpPut]
        [Route("{Id}")]
        public async Task <IActionResult> Update([FromRoute] int Id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel1 = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id);

            if (stockModel1 == null)
            {
                return NotFound();
            }
            stockModel1.Symbol = updateDto.Symbol;
            stockModel1.CompanyName = updateDto.CompanyName;
            stockModel1.Purchase = updateDto.Purchase;
            stockModel1.lastDiv = updateDto.LastDiv;
            stockModel1.Industry = updateDto.Industry;
            stockModel1.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel1.ToStockDto());

        }
        [HttpDelete]
        [Route("{Id}")]

        public async Task <IActionResult> Delete([FromRoute] int Id)
        {
            var stockModel1 = _context.Stocks.FirstOrDefault(x => x.Id == Id);
            if (stockModel1 == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel1);
           await  _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
