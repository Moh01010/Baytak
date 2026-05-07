using Baytak.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class ImageService : IImageService
    {
        public Task DeleteImageAsync(string imageUrl)
        {
            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                imageUrl.TrimStart('/')
            );

            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return Task.CompletedTask;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath=Path.Combine(folderPath, fileName);

            using var stream= new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/{fileName}";
        }
    }
}
