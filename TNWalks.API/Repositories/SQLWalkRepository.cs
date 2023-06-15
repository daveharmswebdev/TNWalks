using Microsoft.EntityFrameworkCore;
using TNWalks.API.Data;
using TNWalks.API.Models.Domain;

namespace TNWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly TnWalksDbContext _dbContext;

        public SQLWalkRepository(TnWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllWalks()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkToUpdate = await _dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (walkToUpdate == null)
            {
                return null;
            }

            walkToUpdate.Name = walk.Name;
            walkToUpdate.Description = walk.Description;
            walkToUpdate.LengthInKm = walk.LengthInKm;
            
            if (walk.WalkImageUrl != null)
            {
                walkToUpdate.WalkImageUrl = walk.WalkImageUrl;
            }

            walkToUpdate.DifficultyId = walk.DifficultyId;
            walkToUpdate.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return walkToUpdate;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walkToDelete = await _dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (walkToDelete == null)
            {
                return null;
            }

            _dbContext.Remove(walkToDelete);
            await _dbContext.SaveChangesAsync();

            return walkToDelete;
        }
    }
}