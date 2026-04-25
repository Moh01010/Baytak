using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Message
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
        public string SenderId { get; set; }

        public bool IsSeen { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
