using TNWalks.API.Models.Domain;

namespace TNWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalks();
        Task<Walk?> GetWalkById(Guid id); 
        Task<Walk> CreateWalkAsync(Walk walk);
    }
}