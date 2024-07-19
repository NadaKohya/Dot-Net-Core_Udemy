using System.Collections.Generic;
using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionDto regionDto)
        {
            if (ModelState.IsValid)
            {
               Region region = mapper.Map<Region>(regionDto);
                await regionRepository.Create(region);
                return Ok("Created");
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Region> regions = await regionRepository.GetAll();
                List<RegionDto> regionDtos = mapper.Map<List<RegionDto>>(regions);
                return Ok(regionDtos);
            }
            catch
            {
                return BadRequest("Error in getting all regions");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Region region = await regionRepository.GetById(id);
            if (region != null)
            {

                RegionDto regionDto = mapper.Map<RegionDto>(region);
                return Ok(regionDto);
            }
            return NotFound("Can't find the wanted region");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RegionDto regionDto)
        {
            if (ModelState.IsValid)
            {
                Region region = mapper.Map<Region>(regionDto);
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

