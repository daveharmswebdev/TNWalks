using Microsoft.EntityFrameworkCore;
using TNWalks.API.Data;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly TnWalksDbContext _dbContext;

        public SQLRegionRepository(TnWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regionToUpdate = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionToUpdate == null)
            {
                return null;
            }
            
            regionToUpdate.Code = region.Code;
            regionToUpdate.Name = region.Name;
            regionToUpdate.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return regionToUpdate;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var regionToDelete = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionToDelete == null)
            {
                return null;
            }

            _dbContext.Remove(regionToDelete);
            await _dbContext.SaveChangesAsync();

            return regionToDelete;
        }
    }
}