using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stock_fincance_api.Data;

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
            var stocks = _context.Stocks.ToList();
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stocks = _context.Stocks.Find(id);
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);


        }
    }
}
