using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Baytak.Application.DTOs.Favorite;
namespace Baytak.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task AddAsync(Guid PropertyId,string userId);
        Task DeleteAsync(Guid PropertyId,string userId);
        Task<IEnumerable<FavoritePropertyDto>> GetAsync(string userId);
    }
}
