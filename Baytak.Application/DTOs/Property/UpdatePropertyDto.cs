using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Property
{
    public class UpdatePropertyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }

        public string City { get; set; }
        public string Address { get; set; }
    }
}
