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
    public class SousCategorieOeuvresController : ControllerBase
    {
        private readonly DataContext _context;

        public SousCategorieOeuvresController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SousCategorieOeuvres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SousCategorieOeuvre>>> GetSousCategoriesOeuvre()
        {
            return await _context.SousCategoriesOeuvre
                .Include(c=>c.Categorie)
                .ToListAsync();
        }

        // GET: api/SousCategorieOeuvres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SousCategorieOeuvre>> GetSousCategorieOeuvre(int id)
        {
            var sousCategorieOeuvre = await _context.SousCategoriesOeuvre.FindAsync(id);

            if (sousCategorieOeuvre == null)
            {
                return NotFound();
            }

            return sousCategorieOeuvre;
        }

        // PUT: api/SousCategorieOeuvres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSousCategorieOeuvre(int id, SousCategorieOeuvre sousCategorieOeuvre)
        {
            if (id != sousCategorieOeuvre.Id)
            {
                return BadRequest();
            }

            _context.Entry(sousCategorieOeuvre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SousCategorieOeuvreExists(id))
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

        // POST: api/SousCategorieOeuvres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SousCategorieOeuvre>> PostSousCategorieOeuvre(SousCategorieOeuvre sousCategorieOeuvre)
        {
            _context.SousCategoriesOeuvre.Add(sousCategorieOeuvre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSousCategorieOeuvre", new { id = sousCategorieOeuvre.Id }, sousCategorieOeuvre);
        }

        // DELETE: api/SousCategorieOeuvres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSousCategorieOeuvre(int id)
        {
            var sousCategorieOeuvre = await _context.SousCategoriesOeuvre.FindAsync(id);
            if (sousCategorieOeuvre == null)
            {
                return NotFound();
            }

            _context.SousCategoriesOeuvre.Remove(sousCategorieOeuvre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SousCategorieOeuvreExists(int id)
        {
            return _context.SousCategoriesOeuvre.Any(e => e.Id == id);
        }
    }
}
