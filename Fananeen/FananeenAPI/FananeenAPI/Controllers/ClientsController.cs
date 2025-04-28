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
    public class ClientsController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients
                .Include(c=>c.ville)
                .ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> Register(Client client)
        {
            
            var city = _context.Villes.FirstOrDefault(c=>c.Id==client.ville.Id);
            client.ville = city;
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.UserName == request.Username);

            if (client == null || !BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new { Message = "Login successful", Client = client });
        }



        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/ChangeNom")]
        public async Task<IActionResult> ChangeNom(int id, string Nom)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Nom = Nom;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //
        [HttpPut("{id}/ChangePrenom")]
        public async Task<IActionResult> ChangePrenom(int id, string Prenom)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Prenom = Prenom;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //
        [HttpPut("{id}/ChangeAddress")]
        public async Task<IActionResult> ChangeBio(int id, string address)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Addres = address;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/ChangePostCode")]
        public async Task<IActionResult> ChangePostCode(int id, int postcode)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Postcode = postcode;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //
        [HttpPut("{id}/ChangeMail")]
        public async Task<IActionResult> ChangeMail(int id, string Mail)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Email = Mail;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //
        [HttpPut("{id}/ChangeTelephone")]
        public async Task<IActionResult> ChangeTelephone(int id, string Telephone)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Telephone = Telephone;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //
        [HttpPut("{id}/ChangeUserName")]
        public async Task<IActionResult> ChangeUsername(int id, string UserName)
        {
            var client = await _context.Clients.FindAsync(id);
            client.UserName = UserName;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, string password)
        {
            var client = await _context.Clients.FindAsync(id);
            var pass = BCrypt.Net.BCrypt.HashPassword(password);
            client.Password = pass;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/IncrementNbrCommisionListed")]
        public async Task<IActionResult> IncrementNbrCommisionListed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            client.NbrCommisionListed++;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/IncrementReviewsMade")]
        public async Task<IActionResult> IncrementReviewsMade(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            client.ReviewsMade++;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/IncrementArtistsBooked")]
        public async Task<IActionResult> IncrementArtistsBooked(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            client.ArtistsBooked++;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/IncrementWorksBought")]
        public async Task<IActionResult> IncrementWorksBought(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            client.WorksBought++;
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/ChangeVille")]
        public async Task<IActionResult> ChangeVille(int id, int villeId)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            var ville = await _context.Villes.FindAsync(villeId);
            if (ville == null)
            {
                return NotFound();
            }

            client.ville = ville;  
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
