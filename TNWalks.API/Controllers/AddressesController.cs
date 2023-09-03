using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TNWalks.API.Component;
using TNWalks.API.Models.Models;

namespace TNWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressComponent _addressComponent;

        public AddressesController(IAddressComponent addressComponent)
        {
            _addressComponent = addressComponent;
        }

        [HttpGet]
        public async Task<ActionResult<List<AddressModel>>> GetAll()
        {
            var addressModels = await _addressComponent.GetAddresses();
            return Ok(addressModels);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("{id:int}")]
        public async Task<ActionResult<AddressModel>> GetById([FromRoute] int id)
        {
            var addressModel = await _addressComponent.GetAddressById(id);
            return Ok(addressModel);
        }
    }
}