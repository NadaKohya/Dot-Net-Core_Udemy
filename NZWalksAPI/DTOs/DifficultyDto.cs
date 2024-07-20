using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class DifficultyDto
	{
        [Required]
        [MinLength(3, ErrorMessage = "Required at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Length can't exceed 3 characters")]
        public string Name { get; set; }
    }
}

