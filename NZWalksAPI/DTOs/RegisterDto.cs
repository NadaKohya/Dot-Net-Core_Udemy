using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class RegisterDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Required at least 8 characters")]
        [MaxLength(15, ErrorMessage = "Length can't exceed 15 characters")]
        public string Password { get; set; }
		[Required]
		[Compare("Password")]
		public string ConfirmedPassword { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
	}
}

