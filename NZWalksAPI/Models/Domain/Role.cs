using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
	public class Role
	{
		[Required]
		public string Name { get; set; }
	}
}

