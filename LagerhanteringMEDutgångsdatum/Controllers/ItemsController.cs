using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LagerhanteringMEDutgångsdatum.Models;

namespace LagerhanteringMEDutgångsdatum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly LagerhanteringContext _context;

        public ItemsController(LagerhanteringContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet("gett-all")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("get-by/{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var targetItem = await _context.Items.FindAsync(id);

            if (targetItem == null)
            {
                return NotFound();
            }

            return targetItem;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItemById(int id, UpdateItemDto dto)
        {
            if (id != dto.ItemId)
                return BadRequest("ID mismatch");

            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            // Uppdatera värden
            item.Name = dto.Name;
            item.Quantity = dto.Quantity;
            item.ExpiryDate = dto.ExpiryDate;
            item.CategoryId = dto.CategoryId;
            item.UserId = dto.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Items.Any(e => e.ItemId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


            // POST: api/Items
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost("create-item")]
        public async Task<ActionResult<Item>> CreateItem(CreateItemDto dto)
        {
            var item = new Item
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                ExpiryDate = dto.ExpiryDate,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemById", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteItemById(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckIfItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
