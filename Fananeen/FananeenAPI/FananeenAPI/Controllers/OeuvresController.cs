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
    public class OeuvresController : ControllerBase
    {
        private readonly DataContext _context;

        public OeuvresController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Oeuvres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oeuvre>>> GetOeuvres()
        {
            return await _context.Oeuvres
                .Include(c => c.Artist)
                    .ThenInclude(c=>c.ville)
                .Include(c => c.Artist.artForm)
                .Include(c=>c.Scategorie)
                    .ThenInclude(c=>c.Categorie)
                .ToListAsync();
        }
        [HttpPut("{id}/ToggleAvailability")]
        public async Task<IActionResult> ChangeArtForm(int id, StatutOeuvre statu)
        {
            var oeuvre = _context.Oeuvres.Find(id);
            oeuvre.Statut=statu;
            _context.Entry(oeuvre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // GET: api/Oeuvres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oeuvre>> GetOeuvre(int id)
        {
            var oeuvre = await _context.Oeuvres.FindAsync(id);

            if (oeuvre == null)
            {
                return NotFound();
            }

            return oeuvre;
        }

        // PUT: api/Oeuvres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOeuvre(int id, Oeuvre oeuvre)
        {
            if (id != oeuvre.Id)
            {
                return BadRequest();
            }

            _context.Entry(oeuvre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OeuvreExists(id))
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

        // POST: api/Oeuvres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Oeuvre>> PostOeuvre(Oeuvre oeuvre)
        {
            var ExcArtist = _context.Artists.FirstOrDefault(c=>c.Id==oeuvre.Artist.Id);
            var ExcScat = _context.SousCategoriesOeuvre.FirstOrDefault(c => c.Id == oeuvre.Scategorie.Id);
            oeuvre.Artist = ExcArtist;
            oeuvre.Scategorie = ExcScat;
            _context.Oeuvres.Add(oeuvre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOeuvre", new { id = oeuvre.Id }, oeuvre);
        }

        // DELETE: api/Oeuvres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOeuvre(int id)
        {
            var oeuvre = await _context.Oeuvres.FindAsync(id);
            if (oeuvre == null)
            {
                return NotFound();
            }

            _context.Oeuvres.Remove(oeuvre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OeuvreExists(int id)
        {
            return _context.Oeuvres.Any(e => e.Id == id);
        }
    }
}
