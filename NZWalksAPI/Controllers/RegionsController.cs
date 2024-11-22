using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.DTOs;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] RegionDto regionDto)
        {
                logger.LogInformation("Create a new region");
            if (ModelState.IsValid)
            {
                Region region = mapper.Map<Region>(regionDto);
                await regionRepository.Create(region);
                return Ok("Created");
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery]  string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            try
            {
                List<Region> regions = await regionRepository.GetAll(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
                List<RegionDto> regionDtos = mapper.Map<List<RegionDto>>(regions);
                return Ok(regionDtos);
            }
            catch
            {
                logger.LogError("Error in getting all regions");
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
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] RegionDto regionDto)
        {
                Region region = mapper.Map<Region>(regionDto);
                Region updatedRegion = await regionRepository.Update(id, region);
                if (updatedRegion != null)
                {
                    return Ok("Updated");
                }
                return NotFound();
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

