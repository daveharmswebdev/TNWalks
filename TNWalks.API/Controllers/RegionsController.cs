using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TNWalks.API.Data;
using TNWalks.API.Models.Domain;
using TNWalks.API.Models.Dtos;
using TNWalks.API.Repositories;

namespace TNWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly TnWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(TnWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();
            var regionDtos = _mapper.Map<List<RegionDto>>(regions);
            return Ok(regionDtos);
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<RegionDto>(region);
            
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion(
            [FromBody] CreateRegionDto regionDto)
        {
            var newRegion = _mapper.Map<Region>(regionDto);

            newRegion = await _regionRepository.CreateRegionAsync(newRegion);

            var actionName = nameof(GetRegionById);
            var routeValues = new { id = newRegion.Id };
            
            var returnDto = _mapper.Map<RegionDto>(newRegion);

            return CreatedAtAction(actionName, routeValues, returnDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,
            [FromBody] CreateRegionDto updateDto)
        {
            var regionToUpdate = _mapper.Map<Region>(updateDto);
            
            regionToUpdate = await _regionRepository.UpdateRegionAsync(id, regionToUpdate);

            if (regionToUpdate == null)
            {
                return NotFound();
            }

            var returnDto = _mapper.Map<RegionDto>(regionToUpdate);

            return Ok(returnDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionToDelete = await _regionRepository.DeleteRegionAsync(id);

            if (regionToDelete == null)
            {
                return NotFound();
            }
            
            var returnDto = _mapper.Map<RegionDto>(regionToDelete);

            return Ok(returnDto);
        }
    }
}
