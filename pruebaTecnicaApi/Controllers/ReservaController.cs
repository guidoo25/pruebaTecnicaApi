using database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnicaApi.Services;


namespace pruebaTecnicaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly CineContext _context;
        private readonly BookingService _bookingService;

        public ReservaController(CineContext context, BookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<BookingEntity>> PostBooking(BookingDto bookingDto)
        {
            var booking = new BookingEntity
            {
                Date = bookingDto.Date,
                CustomerId = bookingDto.CustomerId,
                SeatId = bookingDto.SeatId,
                BillboardId = bookingDto.BillboardId
            };

            _context.BookingEntities.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

    //ejemplo  de consulta  2024-01-01   end:2024-01-30
    [HttpGet("HorrorBookings")]
    public async Task<ActionResult<IEnumerable<BookingEntity>>> GetHorrorBookings(DateTime startDate, DateTime endDate)
    {
        var bookings = await _context.BookingEntities
            .Include(b => b.Billboard)
            .ThenInclude(b => b.Movie)
            .Where(b => b.Date >= startDate && b.Date <= endDate && b.Billboard.Movie.Genre == MovieGenreEnum.HORROR)
            .ToListAsync();

        return bookings;
    }


        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingEntity>> GetBooking(int id)
        {
            var booking = await _context.BookingEntities.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        [HttpPut("cancel/{bookingId}/seat/{seatId}")]
        public async Task<IActionResult> CancelBookingAndInactivateSeat(int bookingId, int seatId)
        {
            try
            {
                await _bookingService.CancelBookingAndInactivateSeat(bookingId, seatId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Un error Ocurrio: " + ex.Message);
            }
        }

        

    }
}