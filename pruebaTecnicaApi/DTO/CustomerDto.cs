using System;
using System.ComponentModel.DataAnnotations;

public class CustomerDto
{
    [Required]
    [MaxLength(20)]
    public string DocumentNumber { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [MaxLength(30)]
    public string Lastname { get; set; }

    [Required]
    public short Age { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }
}