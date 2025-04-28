using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FananeenAPI.Data;
using FananeenAPI.Models;

namespace FananeenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieOeuvresController : ControllerBase
    {
        private readonly DataContext _context;

        public CategorieOeuvresController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CategorieOeuvres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategorieOeuvre>>> GetCategoriesOeuvre()
        {
            return await _context.CategoriesOeuvre.ToListAsync();
        }

        // GET: api/CategorieOeuvres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategorieOeuvre>> GetCategorieOeuvre(int id)
        {
            var categorieOeuvre = await _context.CategoriesOeuvre.FindAsync(id);

            if (categorieOeuvre == null)
            {
                return NotFound();
            }

            return categorieOeuvre;
        }

        // PUT: api/CategorieOeuvres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorieOeuvre(int id, CategorieOeuvre categorieOeuvre)
        {
            if (id != categorieOeuvre.Id)
            {
                return BadRequest();
            }

            _context.Entry(categorieOeuvre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieOeuvreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CategorieOeuvres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategorieOeuvre>> PostCategorieOeuvre(CategorieOeuvre categorieOeuvre)
        {
            _context.CategoriesOeuvre.Add(categorieOeuvre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorieOeuvre", new { id = categorieOeuvre.Id }, categorieOeuvre);
        }

        // DELETE: api/CategorieOeuvres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorieOeuvre(int id)
        {
            var categorieOeuvre = await _context.CategoriesOeuvre.FindAsync(id);
            if (categorieOeuvre == null)
            {
                return NotFound();
            }

            _context.CategoriesOeuvre.Remove(categorieOeuvre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieOeuvreExists(int id)
        {
            return _context.CategoriesOeuvre.Any(e => e.Id == id);
        }
    }
}
