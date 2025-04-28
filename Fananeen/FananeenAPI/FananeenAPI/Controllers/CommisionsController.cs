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
    public class CommisionsController : ControllerBase
    {
        private readonly DataContext _context;

        public CommisionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Commisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commision>>> GetCommisions()
        {
            return await _context.Commisions
                .Include(c=>c.Client)
                .ThenInclude(c => c.ville)
                .Include(c => c.SousCategorieOeuvre)
                .ThenInclude(c=>c.Categorie)
                .ToListAsync();
        }
        // POST: api/Commisions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commision>> PostCommision(Commision commision)
        {
            var sousC = _context.SousCategoriesOeuvre.FirstOrDefault(c => c.Id == commision.SousCategorieOeuvre.Id);
            var client = _context.Clients.FirstOrDefault(c => c.Id== commision.Client.Id);
            commision.SousCategorieOeuvre = sousC;
            commision.Client = client;
            _context.Commisions.Add(commision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommision", new { id = commision.Id }, commision);
        }
        [HttpPut("{id}/ToggleDone")]
        public async Task<IActionResult> ToggleDone(int id)
        {
            var com = await _context.Commisions.FindAsync(id);
            com.IsDone = !com.IsDone;
            _context.Entry(com).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/Commisions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommision(int id)
        {
            var commision = await _context.Commisions.FindAsync(id);
            if (commision == null)
            {
                return NotFound();
            }

            _context.Commisions.Remove(commision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommisionExists(int id)
        {
            return _context.Commisions.Any(e => e.Id == id);
        }
    }
}
