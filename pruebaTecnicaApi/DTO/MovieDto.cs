using System;
using System.ComponentModel.DataAnnotations;
using database;

public class MovieDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public MovieGenreEnum Genre { get; set; }

    [Required]
    public short AllowedAge { get; set; }

    [Required]
    public short LengthMinutes { get; set; }
}