using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class ImageDto
	{
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

