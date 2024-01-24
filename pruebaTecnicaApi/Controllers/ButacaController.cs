using database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnicaApi.DTO;

namespace pruebaTecnicaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButacaController : ControllerBase
    {
        private readonly CineContext _context;

        public ButacaController(CineContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeatEntity>> GetSeat(int id)
        {
            var seat = await _context.SeatEntities.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }

        [HttpPost]
        public async Task<ActionResult<SeatEntity>> PostSeat(SeatDto seatDto)
        {
            var room = await _context.RoomEntities.FindAsync(seatDto.RoomId);
            if (room == null)
            {
                return BadRequest(new { message = "La Sala especificada no existe" });
            }

            var seat = new SeatEntity
            {
                Number = seatDto.Number,
                RowNumber = seatDto.RowNumber,
                RoomId = seatDto.RoomId
            };

            _context.SeatEntities.Add(seat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeat), new { id = seat.Id }, seat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var seat = await _context.SeatEntities.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            _context.SeatEntities.Remove(seat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}