using Baytak.Application.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface IPropertyService
    {
        Task AddAsync(CreatePropertyDto dto,string userId);
        Task UpdateAsync(Guid id, UpdatePropertyDto dto,string userId);
        Task DeleteAsync(Guid id,string userId);
        Task<IEnumerable<PropertyDto>> GetAllAsync();
        Task<PropertyDto> GetByIdAsync(Guid id);
    }
}
