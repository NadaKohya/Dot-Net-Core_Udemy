using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.Data	
{
	public class NZWalksDbContext : IdentityDbContext<ApplicationUser>
    {
        public NZWalksDbContext()
        {
        }
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
		}

		public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }
		public DbSet<Image> Images { get; set; }
    }
}

