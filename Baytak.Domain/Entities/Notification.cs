using Baytak.Domain.Common;
using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class Notification: BaseEntity
    {
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid? ReferenceId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
    }
}
