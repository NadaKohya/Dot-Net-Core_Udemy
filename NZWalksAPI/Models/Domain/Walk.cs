using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
		[ForeignKey("Difficulty")]
        public Guid DifficultyId { get; set; }
		[ForeignKey("Region")]
		public Guid RegionId { get; set; }

		// Navigation properties
		public virtual Difficulty Difficulty { get; set; }
		public virtual Region Region { get; set; }

    }
}

