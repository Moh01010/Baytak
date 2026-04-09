using Baytak.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class Conversation: BaseEntity
    {
        public int PropertyId { get; set; }
        public string UserId { get; set; }
        public string AgentId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Property Property { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser Agent { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
