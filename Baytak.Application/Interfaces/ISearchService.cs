using Baytak.Application.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<PropertyAllDto>> SearchPropertiesAsync(PropertySearchDto filter);
    }
}
