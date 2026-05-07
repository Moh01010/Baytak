using Baytak.Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(string userId,AddNotificationDto dto);
        Task<IEnumerable<NotificationDto>> GetAsync(string userId);
        Task MarkAsReadAsync(Guid id, string userId);

    }
}
