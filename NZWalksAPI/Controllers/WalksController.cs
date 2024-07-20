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
        public async Task<IActionResult> Create([FromBody] WalkDto walkDto)
        {
                Walk walk = mapper.Map<Walk>(walkDto);
                await walkRepository.Create(walk);
                return Ok("Created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walks = await walkRepository.GetAll();
            try
            {
                List<WalkDto> walkDtos = mapper.Map<List<WalkDto>>(walks);
                return Ok(walkDtos);
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
                WalkDto walkDto = mapper.Map<WalkDto>(walk);
                return Ok(walkDto);
            }
            return NotFound("Can't find the wanted walk");
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] WalkUpdateDto walkUpdateDto)
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
