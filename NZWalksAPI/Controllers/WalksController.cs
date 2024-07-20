using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.DTOs;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] WalkDto walkCreationDto)
        {
                Walk walk = mapper.Map<Walk>(walkCreationDto);
                await walkRepository.Create(walk);
                return Ok("Created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            List<Walk> walks = await walkRepository.GetAll(filterOn, filterQuery);
            try
            {
                List<WalkWithNavigationDto> walkWithNavigationDtos = mapper.Map<List<WalkWithNavigationDto>>(walks);
                return Ok(walkWithNavigationDtos);
            }
            catch
            {
                return BadRequest("Error in getting all walks");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Walk walk = await walkRepository.GetById(id);
            if (walk != null)
            {
                WalkWithNavigationDto walkWithNavigationDto = mapper.Map<WalkWithNavigationDto>(walk);
                return Ok(walkWithNavigationDto);
            }
            return NotFound("Can't find the wanted walk");
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] WalkDto walkUpdateDto)
        {
                Walk walk = mapper.Map<Walk>(walkUpdateDto);
                Walk updatedWalk = await walkRepository.Update(id, walk);
                if (updatedWalk != null)
                {
                    return Ok("Updated");
                }
                return NotFound();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Walk walk = await walkRepository.Remove(id);
            if(walk != null)
            {
                return Ok("Removed");
            }
            return NotFound(); 
        }
    }
}
