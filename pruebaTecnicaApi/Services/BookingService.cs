using database;
using Microsoft.EntityFrameworkCore;

namespace pruebaTecnicaApi.Services
{
    public class BookingService
    {
        private readonly CineContext _context;

        public BookingService(CineContext context)
        {
            _context = context;
        }


        public async Task CancelBookingAndInactivateSeat(int bookingId, int seatId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var seat = await _context.SeatEntities.FindAsync(seatId);
                    seat.Status = false;
                    _context.SeatEntities.Update(seat);

                    var booking = await _context.BookingEntities.FindAsync(bookingId);
                    booking.Status = false;
                    _context.BookingEntities.Update(booking);

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
