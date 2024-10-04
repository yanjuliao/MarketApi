using MarketApi.Data;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PaymentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/payments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        return await _context.Payments.Include(p => p.Order).ToListAsync();
    }

    // GET: api/payments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        var payment = await _context.Payments.Include(p => p.Order)
                                             .FirstOrDefaultAsync(p => p.Id == id);

        if (payment == null)
        {
            return NotFound();
        }
        return payment;
    }

    // POST: api/payments
    [HttpPost]
    public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
    }

    // PUT: api/payments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, Payment payment)
    {
        if (id != payment.Id)
        {
            return BadRequest();
        }
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/payments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
