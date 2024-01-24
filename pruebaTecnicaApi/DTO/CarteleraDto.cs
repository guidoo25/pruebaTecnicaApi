
using System.ComponentModel.DataAnnotations;

public class BillboardDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public int RoomId { get; set; }
}