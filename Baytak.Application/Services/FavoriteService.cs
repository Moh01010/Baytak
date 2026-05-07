using Baytak.Application.DTOs.Favorite;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
namespace Baytak.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repo;
        private readonly IPropertyRepository _propertyRepo;

        public FavoriteService(IFavoriteRepository repo, IPropertyRepository propertyRepo)
        {
            _repo = repo;
            _propertyRepo = propertyRepo;
        }

        public async Task AddAsync(Guid PropertyId, string userId)
        {
            var property = await _propertyRepo.GetByIdAsync(PropertyId);
            if (property == null)
                throw new Exception("Property not found");

            if (property.Status == PropertyStatus.Sold)
                throw new Exception("Cannot favorite sold property");

            var result =await _repo.GetById(PropertyId, userId);

            if (result == null)
            {
                var favorite = new Favorite
                {
                    PropertyId = PropertyId,
                    UserId = userId
                };
                await _repo.AddAsync(favorite);
            }
            else
            {
                result.IsDeleted= false;
                result.DeletedAt = null;
                await _repo.UpdateAsync(result);
            }
        }

        public async Task DeleteAsync(Guid PropertyId, string userId)
        {
            var favorite=await _repo.GetById(PropertyId,userId);
            if (favorite == null)
                throw new Exception("Property not found in your favorites");
            else
                await _repo.DeleteAsync(favorite);
        }

        public async Task<IEnumerable<FavoritePropertyDto>> GetAsync(string userId)
        {
            var favorites = await _repo.GetAsync(userId);

            return favorites.Select(f => new FavoritePropertyDto
            {
                PropertyId = f.PropertyId,
                Title = f.Property.Title,
                Price = f.Property.Price,
                City = f.Property.City,
                propertyStatus = f.Property.Status,
                ImageUrl = f.Property.Images.FirstOrDefault()?.ImageUrl ?? "placeholder.jpg"
            });
        }
    }
}
