using api.Data;
using api.Dtos.Stock;
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

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

        if (stockModel == null)
        {
            return NotFound();
        }

        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;

        _context.SaveChanges();
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]

    public IActionResult Delete([FromRoute] int id)
    {
        var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

        if (stockModel == null)
        {
            return NotFound();
        }

        _context.Stocks.Remove(stockModel);
        _context.SaveChanges();

        return NoContent();
    }
    
}







