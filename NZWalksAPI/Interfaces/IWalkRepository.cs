using System;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Interfaces
{
	public interface IWalkRepository
	{
        // it can return null
        Task Create(Walk walk);
        Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        // it can return null
        Task<Walk?> GetById(Guid id);
        // it can return null
        Task<Walk?> Update(Guid id, Walk walk);
        // it can return null
        Task<Walk?> Remove(Guid id);
    }
}

