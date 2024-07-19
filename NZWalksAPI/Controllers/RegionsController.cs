using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.DTOs;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegionDto regionDto)
        {
            if (ModelState.IsValid)
            {
                Region region = new Region()
                {
                    Code = regionDto.Code,
                    Name = regionDto.Name,
                    RegionImageUrl = regionDto.RegionImageUrl
                };
                await regionRepository.Create(region);
                return Ok("Saved");
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Region> regions = await regionRepository.GetAll();
                List<RegionDto> regionsDto = new List<RegionDto>();
                foreach (var region in regions)
                {
                    regionsDto.Add(new RegionDto()
                    {
                        Code = region.Code,
                        Name = region.Name,
                        RegionImageUrl = region.RegionImageUrl
                    });
                }
                return Ok(regionsDto);
            }
            catch
            {
                return BadRequest("Error in getting regions");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Region region = await regionRepository.GetById(id);
            if (region != null)
            {
                RegionDto regionDto = new RegionDto()
                {
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                };
                return Ok(regionDto);
            }
            return NotFound("Can't find the wanted region");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RegionDto regionDto)
        {
            if (ModelState.IsValid)
            {
                Region region = new Region();
                region.Code = regionDto.Code;
                region.Name = regionDto.Name;
                region.RegionImageUrl = regionDto.RegionImageUrl;
                Region updatedRegion = await regionRepository.Update(id, region);
                if (updatedRegion != null)
                {
                    return Ok("Updated");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Region deletedRegion = await regionRepository.Remove(id);
            if (deletedRegion != null)
            {
                return Ok("Removed");
            }
            return NotFound();
        }
    }
}

