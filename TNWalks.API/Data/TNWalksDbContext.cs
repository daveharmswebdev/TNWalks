using Microsoft.EntityFrameworkCore;
using TNWalks.API.Models.Domain;

namespace TNWalks.API.Data
{
    public class TnWalksDbContext : DbContext
    {
        public TnWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        { }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}