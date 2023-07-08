using Microsoft.EntityFrameworkCore;
using TNWalks.Domain.Entities;

namespace TNWalks.API.Data
{
    public class TnWalksDbContext : DbContext
    {
        public TnWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        { }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("fd271b89-4949-423d-a975-e9ed523065e5"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("a3edbed0-6920-4f66-a3d5-5d55ee5fe4ab"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("0c740696-c55b-43ba-b729-304dfb376cdd"),
                    Name = "Difficult"
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("f509a56f-292f-4df0-b4ac-2aa58a10697a"),
                    Name = "Nashville Region",
                    Code = "NR",
                    RegionImageUrl = "https://picsum.photos/200/300"
                },
                new Region()
                {
                    Id = Guid.Parse("2f30f07d-58e2-4a66-b343-af3ca22fc58e"),
                    Name = "Clarksville Region",
                    Code = "CR",
                    RegionImageUrl = "https://picsum.photos/200/300"
                },
                new Region()
                {
                    Id = Guid.Parse("b2105bdd-c2a1-49d5-ba18-326bf12ae6e2"),
                    Name = "Knoxville Region",
                    Code = "UT",
                    RegionImageUrl = "https://picsum.photos/200/300"
                },
                new Region()
                {
                    Id = Guid.Parse("8d6865c7-436c-46bf-a3e3-9e0c18212db4"),
                    Name = "Smokey Mountains",
                    Code = "SM",
                    RegionImageUrl = "https://picsum.photos/200/300"
                },
            };
            
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
    
}