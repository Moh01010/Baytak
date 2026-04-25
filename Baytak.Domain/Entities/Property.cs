using Baytak.Domain.Common;
using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class Property: BaseEntity
    {
        public string Title {  get; set; }
        public string Description { get; set; }

        public decimal  Price { get; set; }

        public PropertyType Type { get; set; }
        public PropertyStatus Status {  get; set; }

        public int Rooms {  get; set; }
        public double Area {  get; set; }

        public string City {  get; set; }
        public string Address {  get; set; }

        public string AgentId { get; set; }

        public ApplicationUser Agent { get; set; }
        public ICollection<PropertyImage> Images { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
