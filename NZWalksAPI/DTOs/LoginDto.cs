using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class LoginDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
    }
}

