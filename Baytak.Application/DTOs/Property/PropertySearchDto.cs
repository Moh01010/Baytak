using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Property
{
    public class PropertySearchDto
    {
        public string? City { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Bedrooms { get; set; }
        //public PropertyType? Type { get; set; } 
        public string? SearchTerm { get; set; }
    }
}
