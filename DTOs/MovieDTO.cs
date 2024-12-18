﻿using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class MovieDTO
    {
        [Required]
        [StringLength(255)]
        public required string Name { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        public GenreDTO Genre { get; set; }
        [Required]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        [Display(Name = "Number in stock")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }
    }
}
