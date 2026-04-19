using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Favorite favorite)
        {
            favorite.IsDeleted = true;
            favorite.DeletedAt = DateTime.UtcNow;

            _context.Favorites.Update(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Favorite>> GetAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Property)
                .ThenInclude(p => p.Images)
                .Where(f=>f.UserId == userId&&!f.IsDeleted).ToListAsync();
        }

        public async Task<Favorite?> GetById(Guid propertyId, string userId)
        {
            return await _context.Favorites
                .Where(f=>!f.IsDeleted)
                .FirstOrDefaultAsync(f=>f.PropertyId == propertyId && f.UserId==userId);
        }

        public async Task UpdateAsync(Favorite favorite)
        {
            _context.Favorites.Update(favorite);
            await _context.SaveChangesAsync();
        }
    }
}
