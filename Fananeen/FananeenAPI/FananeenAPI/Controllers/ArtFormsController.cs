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
    public class ArtFormsController : ControllerBase
    {
        private readonly DataContext _context;

        public ArtFormsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ArtForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtForm>>> GetArtForms()
        {
            return await _context.ArtForms.ToListAsync();
        }

        // GET: api/ArtForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtForm>> GetArtForm(int id)
        {
            var artForm = await _context.ArtForms.FindAsync(id);

            if (artForm == null)
            {
                return NotFound();
            }

            return artForm;
        }

        // PUT: api/ArtForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtForm(int id, ArtForm artForm)
        {
            if (id != artForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(artForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtFormExists(id))
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

        // POST: api/ArtForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtForm>> PostArtForm(ArtForm artForm)
        {
            _context.ArtForms.Add(artForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtForm", new { id = artForm.Id }, artForm);
        }

        // DELETE: api/ArtForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtForm(int id)
        {
            var artForm = await _context.ArtForms.FindAsync(id);
            if (artForm == null)
            {
                return NotFound();
            }

            _context.ArtForms.Remove(artForm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtFormExists(int id)
        {
            return _context.ArtForms.Any(e => e.Id == id);
        }
    }
}
