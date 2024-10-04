using MarketApi.Data;
using MarketApi.Models;
using MarketApi.DTOs; // Certifique-se de adicionar este namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name             
            })
            .ToListAsync();

        return Ok(categories); // Retorna 200 OK com a lista de categorias
    }


    // GET: api/categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound(new { Message = "Categoria não encontrada." });
        }

        return Ok(category); // Retorna 200 OK com a categoria encontrada
    }

    // POST: api/categories
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = new Category
        {
            Name = createCategoryDto.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDto);
    }

    // PUT: api/categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        if (!_context.Categories.Any(e => e.Id == id))
        {
            return BadRequest(new { Message = "ID da categoria não corresponde." });
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound(new { Message = "Categoria não encontrada." });
        }

        category.Name = updateCategoryDto.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { Message = "Erro ao atualizar a categoria." });
        }

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };

        return Ok(categoryDto); // Retorna o DTO da categoria atualizada
    }

    // DELETE: api/categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound(new { Message = "Categoria não encontrada." });
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent(); // Retorna 204 No Content
    }
}
