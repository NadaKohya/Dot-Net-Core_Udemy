using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.DTOs;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Domain;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalksAPI.Controllers
{
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost("Upload")]
        public async Task<IActionResult> Uplaod([FromForm] ImageDto imageDto)
        {
            ValidateUploadedFile(imageDto);
            if (ModelState.IsValid)
            {
                Image image = new Image
                {
                    File = imageDto.File,
                    Name = imageDto.Name,
                    Extension = Path.GetExtension(imageDto.File.FileName),
                    Description = imageDto.Description,
                    Size = imageDto.File.Length,
                };

                await imageRepository.Upload(image);
            }
            return BadRequest(ModelState);
        }

        private void ValidateUploadedFile(ImageDto imageDto)
        {
            string[] allowedExtensions = new string[]  { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file type");
            }

            if(imageDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Uploaded file exceeds maximum allowed size");
            }
        }
    }
}

