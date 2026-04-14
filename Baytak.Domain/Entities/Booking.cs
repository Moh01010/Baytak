using Baytak.Domain.Common;
using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class Booking: BaseEntity
    {
        public string UserId { get; set; }
        public Guid PropertyId { get; set; }

        public DateTime Date { get; set; }
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
        public Property Property { get; set; }
    }
}
