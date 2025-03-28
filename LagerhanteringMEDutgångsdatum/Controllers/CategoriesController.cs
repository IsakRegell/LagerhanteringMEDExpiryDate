using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LagerhanteringMEDutgångsdatum.Models;
using AutoMapper;
using LagerhanteringMEDutgångsdatum.DTOs;

namespace LagerhanteringMEDutgångsdatum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly LagerhanteringContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(LagerhanteringContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Categories
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        // GET: api/Categories/5
        [HttpGet("get-by/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.CategorieId)
            {
                return BadRequest("ID i URL matchar inte ID i body.");
            }

            // Hämta befintlig entitet från databasen
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Uppdatera värden från DTO till entitet
            _mapper.Map(categoryDto, existingCategory);

            // Spara ändringar
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }


        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-categorie")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            // Mappa DTO till databasmodell
            var category = _mapper.Map<Category>(categoryDto);

            // Lägg till och spara
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Mappa tillbaka till DTO inkl. nya ID:t
            var resultDto = _mapper.Map<CategoryDto>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = resultDto.CategorieId }, resultDto);
        }


        // DELETE: api/Categories/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Hämta relaterade items
            var itemsToRemove = _context.Items.Where(i => i.CategoryId == id);
            _context.Items.RemoveRange(itemsToRemove);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategorieId == id);
        }
    }
}
