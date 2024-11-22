using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.Data
{
	public class NZWalksAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<ApplicationUser>()
        .HasIndex(u => u.Email)
        .IsUnique();*/

            /*base.OnModelCreating(builder);
            var userRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var adminRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                 new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };*/
        }
    }
}

