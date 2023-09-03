using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TNWalks.API.Data;
using TNWalks.API.Models.Models;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace TNWalks.API.Component
{
    public class AddressComponent : IAddressComponent
    {
        private readonly TnWalksDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;

        public AddressComponent(TnWalksDbContext dbContext, IConfigurationProvider configurationProvider)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }
        
        public async Task<List<AddressModel>> GetAddresses()
        {
            return await _dbContext.Addresses
                .ProjectTo<AddressModel>(_configurationProvider)
                .ToListAsync();
        }

        public async Task<AddressModel?> GetAddressById(int id)
        {
            return await _dbContext.Addresses
                .ProjectTo<AddressModel>(_configurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}