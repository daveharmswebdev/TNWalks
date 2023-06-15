using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TNWalks.API.Data;
using TNWalks.API.Models.Domain;
using TNWalks.API.Models.Dtos;

namespace TNWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly TnWalksDbContext _dbContext;

        public RegionsController(TnWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _dbContext.Regions.ToListAsync();
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
            var region = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

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

            await _dbContext.Regions.AddAsync(newRegion);
            await _dbContext.SaveChangesAsync();

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
            var regionToUpdate = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionToUpdate == null)
            {
                return NotFound();
            }

            regionToUpdate.Code = updateDto.Code;
            regionToUpdate.Name = updateDto.Name;
            regionToUpdate.RegionImageUrl = updateDto.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

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
            var regionToDelete = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Remove(regionToDelete);
            await _dbContext.SaveChangesAsync();
            
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
