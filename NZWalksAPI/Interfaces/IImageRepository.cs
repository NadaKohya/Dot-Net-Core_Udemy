using System;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Interfaces
{
	public interface IImageRepository
	{
		Task Upload(Image image);
	}
}

