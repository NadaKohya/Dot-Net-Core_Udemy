using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class WalkUpdateDto
	{
        [Required]
        [MinLength(3, ErrorMessage = "Required at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Length can't exceed 3 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Required at least 3 characters")]
        [MaxLength(200, ErrorMessage = "Length can't exceed 3 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}

