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
        public async Task<ActionResult<List<WalkDto>>> GetAll(
            [FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortyBy,
            [FromQuery] bool isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var walks = await _repository.GetAllWalks(
                filterOn, filterQuery, sortyBy, isAscending, pageNumber, pageSize);
            return Ok(_mapper.Map<List<WalkDto>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await _repository.GetWalkById(id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walk));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateWalk([FromBody] CreateWalkDto createWalkDto)
        {
            if (ModelState.IsValid)
            {
                var walkToCreate = _mapper.Map<Walk>(createWalkDto);

                walkToCreate = await _repository.CreateWalkAsync(walkToCreate);

                var returnDto = _mapper.Map<WalkDto>(walkToCreate);

                return CreatedAtAction(nameof(GetWalkById), new { id = walkToCreate.Id }, returnDto);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id,
            [FromBody] UpdateWalkDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var walkToUpdate = _mapper.Map<Walk>(updateDto);

            walkToUpdate = await _repository.UpdateWalkAsync(id, walkToUpdate);

            if (walkToUpdate == null)
            {
                return NotFound();
            }

            var returnDto = _mapper.Map<WalkDto>(walkToUpdate);

            return Ok(returnDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkToDelete = await _repository.DeleteWalkAsync(id);

            if (walkToDelete == null)
            {
                return NotFound();
            }

            var returnDto = _mapper.Map<WalkDto>(walkToDelete);

            return Ok(returnDto);
        }
    }
}
