using System;
using System.Collections.Generic;
using System.Text;
using Baytak.Domain.Entities;
namespace Baytak.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Property property);
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property?>GetByIdAsync(Guid id);

    }
}
