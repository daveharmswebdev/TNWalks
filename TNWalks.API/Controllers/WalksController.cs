using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TNWalks.API.Models.Domain;
using TNWalks.API.Models.Dtos;
using TNWalks.API.Repositories;

namespace TNWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _repository;

        public WalksController(IMapper mapper, IWalkRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await _repository.GetAllWalks();
            return Ok(_mapper.Map<List<WalkDto>>(walks));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] CreateWalkDto createWalkDto)
        {
            var walkToCreate = _mapper.Map<Walk>(createWalkDto);

            walkToCreate = await _repository.CreateWalkAsync(walkToCreate);

            var returnDto = _mapper.Map<WalkDto>(walkToCreate);

            return Ok(returnDto);
        }
    }
}
