using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddAsync(Favorite favorite);
        Task DeleteAsync(Favorite favorite);
        Task UpdateAsync(Favorite favorite);
        Task<IEnumerable<Favorite>> GetAsync(string userId);
        Task<Favorite?> GetById(Guid propertyId, string userId);
        
    }
}
