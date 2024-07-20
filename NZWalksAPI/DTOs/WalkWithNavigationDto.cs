using System;
using NZWalksAPI.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.DTOs
{
	public class WalkWithNavigationDto
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        // Navigation properties
        public virtual Difficulty Difficulty { get; set; }
        public virtual Region Region { get; set; }
    }
}

