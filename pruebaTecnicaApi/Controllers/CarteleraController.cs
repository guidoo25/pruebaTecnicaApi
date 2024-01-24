using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using database;

[Route("api/[controller]")]
[ApiController]
public class CarteleraController : ControllerBase
{
    private readonly CineContext _context;

    public CarteleraController(CineContext context)
    {
        _context = context;
    }

    // GET: api/Billboards
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BillboardEntity>>> GetBillboards()
    {
        return await _context.BillboardEntities.ToListAsync();
    }

    // GET: api/Billboards/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BillboardEntity>> GetBillboard(int id)
    {
        var billboard = await _context.BillboardEntities.FindAsync(id);

        if (billboard == null)
        {
            return NotFound();
        }

        return billboard;
    }

    // PUT: api/Billboards/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBillboard(int id, BillboardEntity billboard)
    {
        if (id != billboard.Id)
        {
            return BadRequest();
        }

        _context.Entry(billboard).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BillboardExists(id))
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
[HttpPost]
public async Task<ActionResult<BillboardEntity>> PostBillboard(BillboardDto billboardDto)
{
    var movie = await _context.MovieEntities.FindAsync(billboardDto.MovieId);
    if (movie == null)
    {
        return BadRequest(new { message = "No movie found with the specified ID." });
    }

    var room = await _context.RoomEntities.FindAsync(billboardDto.RoomId);
    if (room == null)
    {
        return BadRequest(new { message = "No room found with the specified ID." });
    }

    var billboard = new BillboardEntity
    {
        Date = billboardDto.Date,
        StartTime = billboardDto.StartTime,
        EndTime = billboardDto.EndTime,
        MovieId = billboardDto.MovieId,
        RoomId = billboardDto.RoomId
    };

    _context.BillboardEntities.Add(billboard);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetBillboard), new { id = billboard.Id }, billboard);
}


    private bool BillboardExists(int id)
    {
        return _context.BillboardEntities.Any(e => e.Id == id);
    }
}