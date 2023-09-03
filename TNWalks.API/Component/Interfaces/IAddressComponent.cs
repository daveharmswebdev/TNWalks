using TNWalks.API.Models.Models;

namespace TNWalks.API.Component
{
    public interface IAddressComponent
    {
        Task<List<AddressModel>> GetAddresses();
        Task<AddressModel?> GetAddressById(int id);
    }
}