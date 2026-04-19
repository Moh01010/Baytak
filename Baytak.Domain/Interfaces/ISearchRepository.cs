using System;
using System.Collections.Generic;
using System.Text;
using Baytak.Domain.Entities;
namespace Baytak.Domain.Interfaces
{
    public interface ISearchRepository
    {
         IQueryable<Property> GetSearchQuery(
             string? City,
             decimal? minPrice,
             decimal? maxPrice,
             int? bedrooms,
             string? searchTerm);
    }
}
