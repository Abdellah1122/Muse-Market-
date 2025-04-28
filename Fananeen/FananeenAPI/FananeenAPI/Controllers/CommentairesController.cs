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
    public class CommentairesController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentairesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Commentaires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaires()
        {
            return await _context.Commentaires
                        .Include(c=>c.client)
                        .ThenInclude(c=>c.ville)
                        .ToListAsync();
        }

        // POST: api/Commentaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commentaire>> PostCommentaire(Commentaire commentaire)
        {
           var client=_context.Clients.FirstOrDefault(c => c.Id == commentaire.client.Id);
            commentaire.client = client;
            _context.Commentaires.Add(commentaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentaire", new { id = commentaire.Id }, commentaire);
        }

        // DELETE: api/Commentaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentaire(int id)
        {
            var commentaire = await _context.Commentaires.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }

            _context.Commentaires.Remove(commentaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentaireExists(int id)
        {
            return _context.Commentaires.Any(e => e.Id == id);
        }
    }
}
