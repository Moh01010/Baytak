using Baytak.Application.DTOs.Message;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class ChatService: IChatService
    {
        private readonly IConversationRepository _conversationRepo;
        private readonly IMessageRepository _messageRepo;
        private readonly IChatNotifier _notifier;

        public ChatService(IConversationRepository conversationRepo, IMessageRepository messageRepo, IChatNotifier notifier)
        {
            _conversationRepo = conversationRepo;
            _messageRepo = messageRepo;
            _notifier = notifier;
        }

        public async Task<Guid> StartConversationAsync(string userId, string agentId)
        {
            if (userId == agentId)
                throw new Exception("You cannot chat with yourself");


            var convo=await _conversationRepo.GetAsync(userId, agentId);
            if(convo != null) 
                return convo.Id;

            var newConvo = new Conversation
            {
                UserId = userId,
                AgentId = agentId
            };

            await _conversationRepo.AddAsync(newConvo);
            return newConvo.Id;
        }
        public async Task SendMessageAsync(SendMessageDto dto, string senderId)
        {
            var convo = await _conversationRepo.GetByIdAsync(dto.ConversationId);

            if (convo == null)
                throw new Exception("Conversation not found");

            if (convo.UserId != senderId && convo.AgentId != senderId)
                throw new Exception("Unauthorized");
            var message = new Message
            {
                ConversationId = dto.ConversationId,
                SenderId = senderId,
                Content = dto.Content,
                MessageType=MessageType.Text,
                CreatedAt = DateTime.UtcNow
            };
            await _messageRepo.AddAsync(message);

            await _notifier.SendMessageAsync(message.ConversationId, new
            {
                message.Content,
                message.SenderId,
                message.CreatedAt
            });
        }
        public async Task<IEnumerable<MessageDto>> GetMessagesAsync(Guid conversationId,string userId)
        {
            var convo = await _conversationRepo.GetByIdAsync(conversationId);

            if (convo == null)
                throw new Exception("Conversation not found");

            if (convo.UserId != userId && convo.AgentId != userId)
                throw new Exception("Unauthorized");

            var messages=await _messageRepo.GetByConversationIdAsync(conversationId);

            if (messages == null)
                throw new Exception("Conversation not found");


            return messages.Select(m=>new MessageDto
            {
                Id=m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                IsSeen = m.IsSeen,
                CreatedAt = m.CreatedAt
            }).ToList();
        }

        public async Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(string userId)
        {
            var convos = await _conversationRepo.GetUserConversationsAsync(userId);

            return convos.Select(c =>
            {
                var lastMessage = c.Messages
                    .OrderByDescending(m => m.CreatedAt)
                    .FirstOrDefault();

                return new ConversationDto
                {
                    Id = c.Id,

                    OtherUserId = c.UserId == userId
                        ? c.AgentId
                        : c.UserId,

                    LastMessage = lastMessage?.Content,
                    LastMessageDate = lastMessage?.CreatedAt ?? DateTime.MinValue
                };
            })
            .OrderByDescending(c => c.LastMessageDate)
            .ToList();
        }
        public async Task MarkAsSeenAsync(Guid conversationId, string userId)
        {
            var convo = await _conversationRepo.GetByIdAsync(conversationId);

            if (convo == null)
                throw new Exception("Conversation not found");

            if (convo.UserId != userId && convo.AgentId != userId)
                throw new Exception("Unauthorized");

            await _messageRepo.MarkAsSeenAsync(conversationId, userId);

            await _notifier.SendMessageAsync(conversationId, new
            {
                type = "seen",
                userId = userId
            });
        }

    }
}
