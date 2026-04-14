using Baytak.Application.DTOs.PropertyImage;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class PropertyImageService :IPropertyImageService
    {
        private readonly IImageService _imageService;
        private readonly IPropertyImageRepository _repo;

        public PropertyImageService(IImageService imageService, IPropertyImageRepository repo)
        {
            _imageService = imageService;
            _repo = repo;
        }

        public async Task DeleteAsync(Guid imageId, string userId)
        {
            var image = await _repo.GetByIdAsync(imageId);

            if (image == null)
                throw new Exception("Image not found");

            if (image.Property.AgentId != userId)
                throw new Exception("Unauthorized");

            await _imageService.DeleteImageAsync(image.ImageUrl);

            await _repo.DeleteAsync(image);
        }

        public async Task<IEnumerable<PropertyImageResponseDto>> GetImagesAsync(GetImageDto dto)
        {
            var images = await _repo.GetByPropertyIdAsync(dto.propertyId);
            return images.Select(i => new PropertyImageResponseDto
            {
                Id = i.Id,
                Url = i.ImageUrl
            }).ToList();
        }

        public async Task<string> UploadAsync(UploadImageDto dto)
        {
            var imageUrl = await _imageService.UploadImageAsync(dto.file);

            var image = new PropertyImage
            {
                PropertyId = dto.propertyId,
                ImageUrl = imageUrl
            };
            await _repo.AddAsync(image);

            return imageUrl;
        }
    }
}
