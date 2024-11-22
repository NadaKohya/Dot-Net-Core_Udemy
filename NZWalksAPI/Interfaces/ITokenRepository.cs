using System;
using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Interfaces
{
	public interface ITokenRepository
	{
		string CreateJWTToken(IdentityUser identityUser, List<string> roles);
	}
}

