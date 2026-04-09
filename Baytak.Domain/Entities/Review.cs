using System;
using System.Collections.Generic;
using System.Text;
using Baytak.Domain.Common;
namespace Baytak.Domain.Entities
{
    public class Review: BaseEntity
    {
        public string UserId { get; set; }
        public string AgentId { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public ApplicationUser Agent { get; set; }
    }
}
