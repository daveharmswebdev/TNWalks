using Microsoft.EntityFrameworkCore;
using TNWalks.API.Data;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly TnWalksDbContext _dbContext;

        public SQLWalkRepository(TnWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllWalks(
            string? filterOn = null, 
            string? fiilterQuery = null, 
            string? sortyBy = null, 
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(fiilterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(fiilterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortyBy) == false)
            {
                if (isAscending)
                {
                    walks = walks.OrderBy(w => w.Name);
                }
                else
                {
                    walks = walks.OrderByDescending(w => w.Name);
                }
            }
            
            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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