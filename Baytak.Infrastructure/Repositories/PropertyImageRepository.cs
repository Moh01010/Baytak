using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly AppDbContext _context;

        public PropertyImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PropertyImage image)
        {
            await _context.PropertyImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PropertyImage image)
        {
            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<PropertyImage> GetByIdAsync(Guid id)
        {
            return await _context.PropertyImages
                .Include(i => i.Property)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(Guid propertyId)
        {
            return await _context.PropertyImages
                .AsNoTracking()
                .Where(i => i.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}
