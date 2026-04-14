using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task AddAsync(PropertyImage image);
        Task <IEnumerable<PropertyImage>> GetByPropertyIdAsync(Guid propertyId);
        Task<PropertyImage> GetByIdAsync(Guid id);
        Task DeleteAsync(PropertyImage image);
    }
}
