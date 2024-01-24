using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public class BookingEntity : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }

        [ForeignKey("Seat")]
        public int SeatId { get; set; }
        public SeatEntity Seat { get; set; }

        [ForeignKey("Billboard")]
        public int BillboardId { get; set; }
        public BillboardEntity Billboard { get; set; }
    }
}
