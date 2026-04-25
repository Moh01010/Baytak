using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task<IEnumerable<Message>> GetByConversationIdAsync(Guid conversationId);
        Task MarkAsSeenAsync(Guid conversationId, string userId);
    }
}
