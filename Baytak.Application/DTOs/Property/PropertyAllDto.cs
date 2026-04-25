using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Property
{
    public class PropertyAllDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }

        public string City { get; set; }
        public int Rooms { get; set; }
        public string MainImageUrl { get; set; }
    }
}
