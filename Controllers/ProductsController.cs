using MarketApi.Data;
using MarketApi.Models;
using MarketApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .ToListAsync();

        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound(new { Message = "Produto não encontrado." });
        }

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = new Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            CategoryId = createProductDto.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Verifica se a categoria existe antes de retornar o DTO
        var category = await _context.Categories.FindAsync(createProductDto.CategoryId);

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = new CategoryDto
            {
                Id = category?.Id ?? 0,
                Name = category?.Name
            }
        };

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        if (!ProductExists(id))
        {
            return NotFound(new { Message = "Produto não encontrado." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null)
        {
            return NotFound(new { Message = "Produto não encontrado." });
        }

        // Atualiza as propriedades do produto existente apenas se novos valores forem fornecidos
        if (updateProductDto.Name != null) existingProduct.Name = updateProductDto.Name;
        if (updateProductDto.Price.HasValue) existingProduct.Price = updateProductDto.Price.Value;
        if (updateProductDto.CategoryId.HasValue) existingProduct.CategoryId = updateProductDto.CategoryId.Value;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { Message = "Erro ao atualizar o produto." });
        }

        var category = await _context.Categories.FindAsync(existingProduct.CategoryId);
        var productDto = new ProductDto
        {
            Id = existingProduct.Id,
            Name = existingProduct.Name,
            Price = existingProduct.Price,
            Category = new CategoryDto
            {
                Id = category?.Id ?? 0,
                Name = category?.Name
            }
        };

        return Ok(productDto);
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { Message = "Produto não encontrado." });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent(); // Retorna 204 No Content após exclusão bem-sucedida
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
