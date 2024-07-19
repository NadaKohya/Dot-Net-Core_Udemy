using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Interfaces
{
	public interface IRegionRepository
	{
        // it can return null
        Task Create(Region region);
        Task<List<Region>> GetAll();
        // it can return null
        Task<Region?> GetById(Guid id);
        // it can return null
        Task<Region?> Update(Guid id, Region region);
        // it can return null
        Task<Region?> Remove(Guid id);
    }
}

