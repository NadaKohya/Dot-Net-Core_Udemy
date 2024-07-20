using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class RegionDto
	{
        [Required]
        [MinLength(3, ErrorMessage = "Required at least 3 characters")]
        [MaxLength(3, ErrorMessage = "Length can't exceed 3 characters")]
        public string Code { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Required at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Length can't exceed 3 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

