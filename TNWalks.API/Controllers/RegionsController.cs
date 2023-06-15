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

        public RegionsController(TnWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();
            var regionDtos = regions.Select(r => new RegionDto
            {
                Id = r.Id,
                Code = r.Code,
                Name = r.Name,
                RegionImageUrl = r.RegionImageUrl
            }).ToList();
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

            var dto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion(
            [FromBody] CreateRegionDto regionDto)
        {
            var newRegion = new Region
            {
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl
            };

            newRegion = await _regionRepository.CreateRegionAsync(newRegion);

            var actionName = nameof(GetRegionById);
            var routeValues = new { id = newRegion.Id };
            
            var returnDto = new RegionDto
            {
                Id = newRegion.Id,
                Code = newRegion.Code,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };

            return CreatedAtAction(actionName, routeValues, returnDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,
            [FromBody] CreateRegionDto updateDto)
        {
            var regionToUpdate = new Region()
            {
                Code = updateDto.Code,
                Name = updateDto.Name,
                RegionImageUrl = updateDto.RegionImageUrl
            };
            
            regionToUpdate = await _regionRepository.UpdateRegionAsync(id, regionToUpdate);

            if (regionToUpdate == null)
            {
                return NotFound();
            }

            var returnDto = new RegionDto
            {
                Id = regionToUpdate.Id,
                Code = regionToUpdate.Code,
                Name = regionToUpdate.Name,
                RegionImageUrl = regionToUpdate.RegionImageUrl
            };

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
            
            var returnDto = new RegionDto
            {
                Id = regionToDelete.Id,
                Code = regionToDelete.Code,
                Name = regionToDelete.Name,
                RegionImageUrl = regionToDelete.RegionImageUrl
            };

            return Ok(returnDto);
        }
    }
}
