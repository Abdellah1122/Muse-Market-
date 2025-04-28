using FananeenAPI.Data;
using FananeenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController : ControllerBase
{
	private readonly DataContext _context;

	public ArtistsController(DataContext context)
	{
		_context = context;
	}

	// GET: api/Artists
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
	{
		return await _context.Artists
			.Include(c=>c.artForm)
            .Include(c => c.ville)
            .ToListAsync();
	}

	// GET: api/Artists/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Artist>> GetArtist(int id)
	{
		var artist = await _context.Artists.FindAsync(id);

		if (artist == null)
		{
			return NotFound();
		}

		return artist;
	}

	// PUT: api/Artists/5/IncrementNbrFoisVisite
	[HttpPut("{id}/IncrementNbrFoisVisite")]
	public async Task<IActionResult> IncrementNbrFoisVisite(int id)
	{
		var artist = await _context.Artists.FindAsync(id);

		if (artist == null)
		{
			return NotFound();
		}

		// Increment NbrFoisVisite
		artist.NbrFoisVisite++;

		_context.Entry(artist).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	// PUT: api/Artists/5/IncrementNbrOeuvreVendu
	[HttpPut("{id}/IncrementNbrOeuvreVendu")]
	public async Task<IActionResult> IncrementNbrOeuvreVendu(int id)
	{
		var artist = await _context.Artists.FindAsync(id);

		if (artist == null)
		{
			return NotFound();
		}

		// Increment NbrOeuvreVendu
		artist.NbrOeuvreVendu++;

		_context.Entry(artist).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	// PUT: api/Artists/5/AddRating
	[HttpPut("{id}/AddRating")]
	public async Task<IActionResult> AddRating(int id, int rating)
	{
		var artist = await _context.Artists.FindAsync(id);

		if (artist == null)
		{
			return NotFound();
		}
		artist.Rating += rating;

		_context.Entry(artist).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return NoContent();
	}
	//
    [HttpPut("{id}/ChangeNom")]
    public async Task<IActionResult> ChangeNom(int id, string Nom)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.Nom = Nom;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
	//
    [HttpPut("{id}/ChangePrenom")]
    public async Task<IActionResult> ChangePrenom(int id, string Prenom)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.Prenom = Prenom;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
	//
    [HttpPut("{id}/ChangeBio")]
    public async Task<IActionResult> ChangeBio(int id, string Bio)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.Bio = Bio;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
	//
    [HttpPut("{id}/ChangeMail")]
    public async Task<IActionResult> ChangeMail(int id, string Mail)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.Email = Mail;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
	//
    [HttpPut("{id}/ChangeTelephone")]
    public async Task<IActionResult> ChangeTelephone(int id, string Telephone)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.Telephone = Telephone;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    //
    [HttpPut("{id}/ChangeUserName")]
    public async Task<IActionResult> ChanveVille(int id, string UserName)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.UserName = UserName;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("{id}/ChangePassword")]
    public async Task<IActionResult> ChangePassword(int id, string password)
    {
        var artist = await _context.Artists.FindAsync(id);
		var pass = BCrypt.Net.BCrypt.HashPassword(password);
        artist.PassWord = pass;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("{id}/ChangeVille")]
    public async Task<IActionResult> ChangeVille(int id, int villeId)
    {
        var artist = await _context.Artists.FindAsync(id);
        if (artist == null)
        {
            return NotFound();
        }

        var ville = await _context.Villes.FindAsync(villeId);
        if (ville == null)
        {
            return NotFound();
        }

        artist.ville = ville;  // Assuming "Ville" is the property representing the relationship with the "Villes" table
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent(); // Return NoContent status to indicate success without a response body
    }
    [HttpPut("{id}/ToggleAvailability")]
    public async Task<IActionResult> ToggleAvailability(int id)
    {
        var artist = await _context.Artists.FindAsync(id);
        artist.IsAvailable = !artist.IsAvailable;

        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpPut("{id}/ChangeArtForm")]
    public async Task<IActionResult> ChangeArtForm(int id, int Artformid)
    {
        var artist = await _context.Artists.FindAsync(id);
        var artform = await _context.ArtForms .FindAsync(Artformid);
        artist.artForm = artform;
        _context.Entry(artist).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    // PUT: api/Artists/5
    [HttpPut("{id}")]
	public async Task<IActionResult> PutArtist(int id, Artist artist)
	{
		if (id != artist.Id)
		{
			return BadRequest();
		}

		_context.Entry(artist).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!ArtistExists(id))
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

	// POST: api/Artists
	[HttpPost]
	public async Task<ActionResult<Artist>> Register(Artist artist)
	{
		var excVille = _context.Villes.FirstOrDefault(c=>c.Id == artist.ville.Id);
		var excArtform = _context.ArtForms.FirstOrDefault(c=>c.Id==artist.artForm.Id);
		artist.ville = excVille;
		artist.artForm = excArtform;
		artist.PassWord=BCrypt.Net.BCrypt.HashPassword(artist.PassWord);
		_context.Artists.Add(artist);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
	}

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var artist = await _context.Artists
            .FirstOrDefaultAsync(c => c.UserName == request.Username);

        if (artist == null || !BCrypt.Net.BCrypt.Verify(request.Password, artist.PassWord))
        {
            return Unauthorized("Invalid credentials.");
        }

        return Ok(new { Message = "Login successful", Artist = artist });
    }


    // DELETE: api/Artists/5
    [HttpDelete("{id}")]
	public async Task<IActionResult> DeleteArtist(int id)
	{
		var artist = await _context.Artists.FindAsync(id);

		if (artist == null)
		{
			return NotFound();
		}

		_context.Artists.Remove(artist);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool ArtistExists(int id)
	{
		return _context.Artists.Any(e => e.Id == id);
	}
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}