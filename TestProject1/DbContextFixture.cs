using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TNWalks.API.Component;
using TNWalks.API.Data;
using TNWalks.API.Mappings;
using TNWalks.Domain.Entities;

namespace TestProject1
{
    public class DbContextFixture : IDisposable
    {
        private TnWalksDbContext Context { get; }
        public AddressComponent AddressComponent { get; } 

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<TnWalksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new TnWalksDbContext(options);
            SeedData();
            
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
            var mapper = new Mapper(configurationProvider);

            AddressComponent = new AddressComponent(Context, mapper.ConfigurationProvider);
            
        }
        
        private void SeedData()
        {
            var addresses = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    Line1 = "123 Street",
                    Line2 = "",
                    City = "Nashville",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    State = "TN",
                    ZipCode = "37214"
                },
                new Address
                {
                    Id = 2,
                    Line1 = "500 Oakland",
                    Line2 = "",
                    City = "Nashville",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    State = "TN",
                    ZipCode = "37214"
                }
            };

            Context.Addresses.AddRange(addresses);
            Context.SaveChanges();
        }
        
        public void Dispose()
        {
            Context.Dispose();
        }
        
        
    }
    
    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<DbContextFixture>
    {
        // This class has no code, and is never created.
        // Its purpose is simply to be the place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.
    }
}