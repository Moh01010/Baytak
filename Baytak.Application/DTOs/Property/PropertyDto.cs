using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Property
{
    public class PropertyDto
    {
        public Guid Id { get; set; }
        public string AgentId {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }

        public int Rooms { get; set; }
        public double Area { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
