using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Message
{
    public class ConversationDto
    {
        public Guid Id { get; set; }

        public string OtherUserId { get; set; }

        public string LastMessage { get; set; }
        public DateTime LastMessageDate { get; set; }
    }
}
