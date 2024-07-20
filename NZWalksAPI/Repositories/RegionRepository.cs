using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DTOs;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext context;
        public RegionRepository(NZWalksDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Region region)
        {
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();
        }

        public async Task<List<Region>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var regions = context.Regions.AsQueryable();

            // Filtering
            // We will make filtering only by the Name
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = regions.Where(r => r.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(r => r.Name) : regions.OrderByDescending(r => r.Name);
                }
            }

            // Paginating
            var skipedPages = (pageNumber - 1) * pageSize;

            return await regions.Skip(skipedPages).Take(pageSize).ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(region => region.Id == id);
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            Region existedRegion = await context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (existedRegion != null)
            {
                existedRegion.Code = region.Code;
                existedRegion.Name = region.Name;
                existedRegion.RegionImageUrl = region.RegionImageUrl;
                await context.SaveChangesAsync();
            }
            return existedRegion;
        }

        public async Task<Region?> Remove(Guid id)
        {
            Region region = await context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (region != null)
            {
                context.Regions.Remove(region);
                await context.SaveChangesAsync();
            }
            return region;
        }
    }
}

