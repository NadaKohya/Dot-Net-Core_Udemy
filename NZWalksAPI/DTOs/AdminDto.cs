using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTOs
{
	public class AdminDto
	{
        [Required]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

