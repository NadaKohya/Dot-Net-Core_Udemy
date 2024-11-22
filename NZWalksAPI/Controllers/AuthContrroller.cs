using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.DTOs;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthContrroller : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthContrroller(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Route is important to be specified here because we have more than one post method
        [HttpPost("RegisteUser")]
        [ValidateModel]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            IdentityResult identityResult = await userManager.CreateAsync(applicationUser, registerDto.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(applicationUser, "User");
                if (identityResult.Succeeded)
                {
                    return Ok("Registered");
                }
            }
            return BadRequest("Failed to register");
        }

        // I make it as a separated endpoint because user can't set himself as admin while registeration
        // An admin sets some users as admins using their emails
        // Email is unique for each user
        [HttpPost("SetAdmin")]
        [ValidateModel]
        public async Task<IActionResult> SetAdmin([FromBody] AdminDto adminDto)
        {
            ApplicationUser applicationUser = await userManager.FindByEmailAsync(adminDto.Email);
            if(applicationUser != null)
            {
                IdentityResult identityResult = await userManager.RemoveFromRoleAsync(applicationUser, "User");
                if (identityResult.Succeeded)
                {
                    identityResult = await userManager.AddToRoleAsync(applicationUser, "Admin");
                    if (identityResult.Succeeded)
                    {
                        return Ok();
                    }
                }
            }
            return NotFound();
        }
    }
}