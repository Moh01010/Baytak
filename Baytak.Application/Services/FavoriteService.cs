using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Baytak.Application.DTOs.Favorite;
namespace Baytak.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repo;

        public FavoriteService(IFavoriteRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(Guid PropertyId, string userId)
        {
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
                ImageUrl = f.Property.Images.FirstOrDefault()?.ImageUrl ?? "placeholder.jpg"
            });
        }
    }
}
