
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using database;

[Route("api/[controller]")]
[ApiController]
public class SalaController : ControllerBase
{
    private readonly CineContext _context;

    public SalaController(CineContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomEntity>>> GetRooms()
    {
        return await _context.RoomEntities.ToListAsync();
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<RoomEntity>> GetRoom(int id)
    {
        var room = await _context.RoomEntities.FindAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        return room;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(int id, RoomEntity room)
    {
        if (id != room.Id)
        {
            return BadRequest();
        }

        _context.Entry(room).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomExists(id))
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
public async Task<ActionResult<BookingEntity>> PostBooking(BookingDto bookingDto)
{
    var customer = await _context.CustomerEntities.SingleOrDefaultAsync(c => c.Id == bookingDto.CustomerId);
    if (customer == null)
    {
        return BadRequest("CustomerId no existe");
    }

    var billboard = await _context.BillboardEntities.SingleOrDefaultAsync(b => b.Id == bookingDto.BillboardId);
    if (billboard == null)
    {
        return BadRequest("BillboardId no existe");
    }

    var booking = new BookingEntity
    {
        Date = bookingDto.Date,
        CustomerId = bookingDto.CustomerId,
        SeatId = bookingDto.SeatId,
        BillboardId = bookingDto.BillboardId
    };

    _context.BookingEntities.Add(booking);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetRoom), new { id = booking.Id }, booking);
}




    private bool RoomExists(int id)
    {
        return _context.RoomEntities.Any(e => e.Id == id);
    }
}