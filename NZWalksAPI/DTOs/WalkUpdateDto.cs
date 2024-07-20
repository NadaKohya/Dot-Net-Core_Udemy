using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.DTOs
{
	public class WalkUpdateDto
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}

