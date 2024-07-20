using System;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext context;

        public WalkRepository(NZWalksDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Walk walk)
        {
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
        }

        public async Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null)
        {
            var walks = context.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }
            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetById(Guid id)
        {
            return await context.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(walk => walk.Id == id);
        }

        public async Task<Walk?> Update(Guid id, Walk walk)
        {
            Walk existedWalk = await context.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(walk => walk.Id == id);
            if (existedWalk != null)
            {
                existedWalk.Name = walk.Name;
                existedWalk.Description = walk.Description;
                existedWalk.LengthInKm = walk.LengthInKm;
                existedWalk.WalkImageUrl = walk.WalkImageUrl;
                existedWalk.DifficultyId = walk.DifficultyId;
                existedWalk.RegionId = walk.RegionId;
                await context.SaveChangesAsync();
            }
            return existedWalk;
        }

        public async Task<Walk?> Remove(Guid id)
        {
            Walk walk = await context.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(walk => walk.Id == id);
            if(walk != null)
            {
                context.Walks.Remove(walk);
                await context.SaveChangesAsync();
            }
            return walk;
        }
    }
}

