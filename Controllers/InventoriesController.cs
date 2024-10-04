using MarketApi.Data;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class InventoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public InventoriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetInventory()
    {
        return await _context.Inventories.Include(i => i.Product).ToListAsync();
    }

    // GET: api/inventory/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Inventory>> GetInventory(int id)
    {
        var inventory = await _context.Inventories.Include(i => i.Product)
                                                .FirstOrDefaultAsync(i => i.Id == id);
        if (inventory == null)
        {
            return NotFound();
        }
        return inventory;
    }

    // POST: api/inventory
    [HttpPost]
    public async Task<ActionResult<Inventory>> CreateInventory(Inventory inventory)
    {
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInventory), new { id = inventory.Id }, inventory);
    }

    // PUT: api/inventory/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInventory(int id, Inventory inventory)
    {
        if (id != inventory.Id)
        {
            return BadRequest();
        }
        _context.Entry(inventory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/inventory/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventory(int id)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
