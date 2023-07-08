using Microsoft.EntityFrameworkCore;
using TNWalks.API.Data;
using TNWalks.API.Models.Domain;

namespace TNWalks.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly TnWalksDbContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(TnWalksDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}