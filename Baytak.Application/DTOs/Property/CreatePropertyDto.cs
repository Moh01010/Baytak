using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Property
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Area { get; set; }
        public PropertyType PropertyType { get; set; }

        public string City { get; set; }
        public string Address { get; set; }
    }
}
