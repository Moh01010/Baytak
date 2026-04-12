using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;
        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Property property)
        {
            property.IsDeleted = true;
            property.DeletedAt = DateTime.UtcNow;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(Guid id)
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Agent)
                .FirstOrDefaultAsync(p=>p.Id==id&&!p.IsDeleted);
        }

    }
}
