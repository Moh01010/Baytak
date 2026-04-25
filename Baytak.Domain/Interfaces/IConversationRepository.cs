using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetAsync(string userId, string agentId);
        Task AddAsync(Conversation conversation);
        Task<Conversation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Conversation>> GetUserConversationsAsync(string userId);
    }
}
