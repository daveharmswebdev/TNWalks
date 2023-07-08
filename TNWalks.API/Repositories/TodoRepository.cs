using TNWalks.API.Data;
using TNWalks.API.Models.Domain;

namespace TNWalks.API.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TnWalksDbContext context) : base(context)
        {
        }
    }
}