using Baytak.Application.DTOs.Notification;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(string userId, AddNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = dto.Title,
                Content = dto.Content,
                ReferenceId = dto.refId,
                Type=dto.type,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(notification);
        }

        public async Task<IEnumerable<NotificationDto>> GetAsync(string userId)
        {
            var notifications =await _repo.GetByUserIdAsync(userId);
            
            return notifications.Select(n=>new NotificationDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt,
                type=n.Type
            });
        }

        public async Task MarkAsReadAsync(Guid id, string userId)
        {
            var notification=await _repo.GetByIdAsync(id);
            if (notification == null)
                throw new Exception("Notification not found");

            if (notification.UserId != userId)
                throw new UnauthorizedAccessException();

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                await _repo.UpdateAsync(notification);
            }
        }
    }
}
