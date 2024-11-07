using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly ApplicationDbContext _context;
    public StockController(ILogger<StockController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks
            .Select(s => s.ToStockDto())
            .ToList();
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
        return Ok(stocks.ToStockDto());
    }
}







