using TNWalks.API.Data;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TnWalksDbContext context) : base(context)
        {
        }
    }
}