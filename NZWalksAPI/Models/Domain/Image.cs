using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Domain
{
	public class Image
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		[NotMapped]
		public IFormFile File { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public string Extension { get; set; }
		[Required]
		public long Size { get; set; }
		[Required]
		public string path { get; set; }
    }
}

