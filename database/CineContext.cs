using Microsoft.EntityFrameworkCore;

namespace database
{
    public class CineContext : DbContext
    {
        public CineContext(DbContextOptions<CineContext> options) : base(options)
        {
        }
        //public DbSet<BaseEntity> BaseEntities { get; set; }
        public DbSet<BillboardEntity> BillboardEntities { get; set; }
        public DbSet<BookingEntity> BookingEntities { get; set; }
        public DbSet<CustomerEntity> CustomerEntities { get; set; }
        public DbSet<MovieEntity> MovieEntities { get; set; }
        public DbSet<RoomEntity> RoomEntities { get; set; }
        public DbSet<SeatEntity> SeatEntities { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<BillboardEntity>()
        .HasOne(b => b.Movie)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction); 

    modelBuilder.Entity<BillboardEntity>()
        .HasOne(b => b.Room)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction); 

    modelBuilder.Entity<BookingEntity>()
        .HasOne(b => b.Billboard)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction); 

    modelBuilder.Entity<BookingEntity>()
        .HasOne(b => b.Customer)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction); 
    modelBuilder.Entity<SeatEntity>()
        .HasOne(b => b.Room)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction);
    modelBuilder.Entity<BookingEntity>()
    .HasOne(b => b.Seat)
    .WithMany()
    .OnDelete(DeleteBehavior.NoAction);
    
}
    }
}