using database;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PeliculaController : ControllerBase
{
    private readonly CineContext _context;

    public PeliculaController(CineContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<MovieEntity>> PostMovie(MovieDto movieDto)
    {
        var movie = new MovieEntity
        {
            Name = movieDto.Name,
            Genre = movieDto.Genre,
            AllowedAge = movieDto.AllowedAge,
            LengthMinutes = movieDto.LengthMinutes
        };

        _context.MovieEntities.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<MovieEntity>> GetMovie(int id)
    {
        var movie = await _context.MovieEntities.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }
}