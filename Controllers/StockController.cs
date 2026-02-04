using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stock_fincance_api.Data;
using stock_fincance_api.DTOs;
using stock_fincance_api.Mappers;

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
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
                 .Select(s => StockMapper.ToStockDto(s));

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return base.Ok(StockMapper.ToStockDto(stock));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel1 = stockDto.ToStockFromCreateDto();
            _context.Stocks.Add(stockModel1);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel1.Id }, stockModel1.ToStockDto());

        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel1 = _context.Stocks.FirstOrDefault(x => x.Id == Id);

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

            _context.SaveChanges();

            return Ok(stockModel1.ToStockDto());

        }
        [HttpDelete]
        [Route("{Id}")]

        public IActionResult Delete([FromRoute] int Id)
        {
            var stockModel1 = _context.Stocks.FirstOrDefault(x => x.Id == Id);
            if (stockModel1 == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel1);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
