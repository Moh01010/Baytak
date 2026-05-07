using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
        Task<Notification?> GetByIdAsync(Guid id);
        Task UpdateAsync(Notification notification);
    }
}
