using database;

namespace pruebaTecnicaApi.Services
{
    public class BillboardServices
    {
        private readonly CineContext _context;

        public BillboardServices( CineContext context ) { 
        _context = context;
        }
        public async Task CancelBillboardAndAllBookings(int billboardId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var billboard = await _context.BillboardEntities.FindAsync(billboardId);
                    if (billboard.Date < DateTime.Now)
                    {
                        throw new Exception("No se puede cancelar funciones de la cartelera con fecha anterior a la actual");
                    }

                    billboard.Status = false;
                    _context.BillboardEntities.Update(billboard);

                    var bookings = _context.BookingEntities.Where(b => b.BillboardId == billboardId);
                    foreach (var booking in bookings)
                    {
                        booking.Status = false;
                        _context.BookingEntities.Update(booking);

                        var seat = await _context.SeatEntities.FindAsync(booking.SeatId);
                        seat.Status = true;
                        _context.SeatEntities.Update(seat);

                        Console.WriteLine($"Cliente alterado: {booking.CustomerId}");
                    }

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

