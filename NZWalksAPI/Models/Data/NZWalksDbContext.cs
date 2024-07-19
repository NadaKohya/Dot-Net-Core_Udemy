using System;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.Data
{
	public class NZWalksDbContext : DbContext
	{
		public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
		}

		public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }
	}
}

