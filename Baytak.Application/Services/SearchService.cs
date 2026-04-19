using Baytak.Application.DTOs.Property;
using Baytak.Application.Interfaces;
using Baytak.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _repo;

        public SearchService(ISearchRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PropertyAllDto>> SearchPropertiesAsync(PropertySearchDto filter)
        {
            var query = _repo.GetSearchQuery(
                filter.City,
                filter.MinPrice,
                filter.MaxPrice,
                filter.Bedrooms,
                filter.SearchTerm
            );

            return await query.Select(p => new PropertyAllDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                City = p.City,
                Bedrooms = p.Bedrooms,
                MainImageUrl = p.Images
                        .Where(i => !i.IsDeleted)
                        .OrderBy(i => i.Id)
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault()
            }).ToListAsync();
        }
    }
}
