using System;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
	public class ImageRepository:IImageRepository
	{
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext context;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        public async Task Upload(Image image)
        {
            // User uploads images to the server from his machine
            // Images will be saved in the followin path in the server
            // webHostEnvironment.ContentRootPath is the path of our API
            string path = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.Name}{image.Extension}");
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await image.File.CopyToAsync(fileStream);

            // https://localhost:7269/images/cool-cat.jpg
            string urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.Name}{image.Extension}";

            // So that any one can use this path again to access the image on the server
            image.path = urlFilePath;

            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();
        }

    }
}

