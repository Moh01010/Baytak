using Baytak.Application.DTOs.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface IChatService
    {
        Task<Guid> StartConversationAsync(string userId, string agentId);
        Task SendMessageAsync(SendMessageDto dto, string senderId);
        Task<IEnumerable<MessageDto>> GetMessagesAsync(Guid conversationId, string userId);
        Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(string userId);
        Task MarkAsSeenAsync(Guid conversationId, string userId);
    }
}
