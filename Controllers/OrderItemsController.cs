using MarketApi.Data;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/orderitems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
    {
        return await _context.OrderItems.Include(oi => oi.Product).Include(oi => oi.Order).ToListAsync();
    }

    // GET: api/orderitems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
    {
        var orderItem = await _context.OrderItems.Include(oi => oi.Product)
                                                 .Include(oi => oi.Order)
                                                 .FirstOrDefaultAsync(oi => oi.Id == id);

        if (orderItem == null)
        {
            return NotFound();
        }
        return orderItem;
    }

    // POST: api/orderitems
    [HttpPost]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItem);
    }

    // PUT: api/orderitems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
    {
        if (id != orderItem.Id)
        {
            return BadRequest();
        }
        _context.Entry(orderItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/orderitems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null)
        {
            return NotFound();
        }
        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
