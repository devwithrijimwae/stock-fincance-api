using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stock_fincance_api.Data;
using stock_fincance_api.DTOs;
using stock_fincance_api.Mappers;
using stock_fincance_api.Repositoy;


namespace stock_fincance_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();

            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
           await _stockRepo.CreateAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

        }
        [HttpPut]
        [Route("{Id}")]
        public async Task <IActionResult> Update([FromRoute] int Id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(Id,updateDto);

            if (stockModel == null)
            {
                return NotFound();
            }
           
            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());

        }
        [HttpDelete]
        [Route("{Id}")]

        public async Task <IActionResult> Delete([FromRoute] int Id)
        {
            var stockModel = await _stockRepo.DeleteAsync(Id);
    
            if (stockModel == null)
            {
                return NotFound();
            }
                return NoContent();

        }
    }
}
