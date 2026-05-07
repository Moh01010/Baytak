using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Notification
{
    public class AddNotificationDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType type { get; set; }
        public Guid? refId { get; set; } = null;
    }
}
