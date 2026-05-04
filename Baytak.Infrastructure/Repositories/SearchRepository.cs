using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;

        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Property> GetSearchQuery(string? City, decimal? minPrice, decimal? maxPrice, int? Rooms, string? searchTerm)
        {
            var query = _context.Properties
                .Where(p => !p.IsDeleted && p.Status == PropertyStatus.Available)
                .AsQueryable();

            if(!string.IsNullOrEmpty(City) )
            {
                query = query.Where(p => p.City.Contains(City));
            }

            if(minPrice.HasValue)
            {
                query=query.Where(p=>p.Price>=minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if(Rooms.HasValue)
            {
                query=query.Where(p=>p.Rooms == Rooms.Value);
            }
            if(!string.IsNullOrEmpty(searchTerm) )
            {
                query=query.Where(p=>p.Title.Contains(searchTerm)||p.Description.Contains(searchTerm));
            }
            return query;
        }
    }
}
