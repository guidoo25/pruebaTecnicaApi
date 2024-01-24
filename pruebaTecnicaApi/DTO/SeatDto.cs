using System.ComponentModel.DataAnnotations;

namespace pruebaTecnicaApi.DTO
{
    public class SeatDto
    {
        [Required]
        public short Number { get; set; }

        [Required]
        public short RowNumber { get; set; }

        [Required]
        public int RoomId { get; set; }
    }
}
