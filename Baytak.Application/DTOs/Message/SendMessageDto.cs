using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Message
{
    public class SendMessageDto
    {
        public Guid ConversationId { get; set; }
        public string Content { get; set; }
    }
}
