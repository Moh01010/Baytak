using Baytak.Domain.Common;
using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class Message: BaseEntity
    {
        public int ConversationId { get; set; }
        public string SenderId { get; set; }

        public string Content {  get; set; }
        public MessageType MessageType { get; set; }

        public bool IsSeen { get; set; }

        public DateTime CreatedAt { get; set; }

        public Conversation Conversation { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
