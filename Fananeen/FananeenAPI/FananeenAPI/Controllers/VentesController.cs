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
    public class VentesController : ControllerBase
    {
        private readonly DataContext _context;

        public VentesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Ventes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vente>>> GetVente()
        {
            return await _context.Vente
                .Include(c=>c.Oeuvre)
                .Include(c => c.Client)
                .ToListAsync();
        }

        // GET: api/Ventes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vente>> GetVente(int id)
        {
            var vente = await _context.Vente.FindAsync(id);

            if (vente == null)
            {
                return NotFound();
            }

            return vente;
        }

        // PUT: api/Ventes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVente(int id, Vente vente)
        {
            if (id != vente.Id)
            {
                return BadRequest();
            }

            _context.Entry(vente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenteExists(id))
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

        // POST: api/Ventes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vente>> PostVente(Vente vente)
        {
            var client = _context.Clients
                .Include(c => c.ville)
                .FirstOrDefault(c => c.Id == vente.Client.Id);

            var oeuvre = _context.Oeuvres
                .Include(o => o.Artist)
                    .ThenInclude(a => a.ville)
                .Include(o => o.Artist.artForm)
                .Include(o => o.Scategorie)
                    .ThenInclude(s => s.Categorie)
                .FirstOrDefault(o => o.Id == vente.Oeuvre.Id);

            vente.Client = client;
            vente.Oeuvre = oeuvre;
            _context.Vente.Add(vente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVente", new { id = vente.Id }, vente);
        }

        // DELETE: api/Ventes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVente(int id)
        {
            var vente = await _context.Vente.FindAsync(id);
            if (vente == null)
            {
                return NotFound();
            }

            _context.Vente.Remove(vente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VenteExists(int id)
        {
            return _context.Vente.Any(e => e.Id == id);
        }
    }
}
