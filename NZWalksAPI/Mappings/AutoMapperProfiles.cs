using System;
using AutoMapper;
using NZWalksAPI.DTOs;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			// We use ReverseMap to map from Region into RegionDto and vice versa
			// If properties names aren't the same in the to objects, we should use ForMember to map them
			CreateMap<Region, RegionDto>().ReverseMap();
			CreateMap<Difficulty, DifficultyDto>().ReverseMap();
			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<Walk, WalkUpdateDto>().ReverseMap();
        }
	}
}

