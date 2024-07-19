using System;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Interfaces
{
	public interface IWalkRepository
	{
        // it can return null
        Task Create(Walk walk);
        Task<List<Walk>> GetAll();
        // it can return null
        Task<Walk?> GetById(Guid id);
        // it can return null
        Task<Walk?> Update(Guid id, Walk walk);
        // it can return null
        Task<Walk?> Remove(Guid id);
    }
}

