using TNWalks.Domain.Entities;

namespace TNWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalks(string? filterOn = null, 
            string? fiilterQuery = null,
            string? sortyBy = null, 
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
        );
        Task<Walk?> GetWalkById(Guid id); 
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}