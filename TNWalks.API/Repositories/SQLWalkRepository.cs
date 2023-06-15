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
            return await _dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }
    }
}