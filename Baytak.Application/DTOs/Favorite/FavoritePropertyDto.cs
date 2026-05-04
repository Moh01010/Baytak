using System;
using System.Collections.Generic;
using System.Text;
using Baytak.Domain.Enums;
namespace Baytak.Application.DTOs.Favorite
{
    public class FavoritePropertyDto
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string City { get; set; }
        public PropertyStatus propertyStatus { get; set; }
    }
}
