using System;
using System.ComponentModel.DataAnnotations;

public class BookingDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int SeatId { get; set; }

    [Required]
    public int BillboardId { get; set; }
}