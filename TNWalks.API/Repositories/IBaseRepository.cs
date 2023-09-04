namespace TNWalks.API.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IQueryable<T>> GetQueryable();
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}