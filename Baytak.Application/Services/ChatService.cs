using Baytak.Application.DTOs.Message;
using Baytak.Application.DTOs.Notification;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly INotificationService _notificationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatService(IConversationRepository conversationRepo, IMessageRepository messageRepo, IChatNotifier notifier, INotificationService notificationService, UserManager<ApplicationUser> userManager)
        {
            _conversationRepo = conversationRepo;
            _messageRepo = messageRepo;
            _notifier = notifier;
            _notificationService = notificationService;
            _userManager = userManager;
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

            var receiverId = message.SenderId == convo.AgentId ? convo.UserId : convo.AgentId;
            var sender = await _userManager.FindByIdAsync(senderId);
            await _notificationService.CreateAsync(
                    receiverId,
                    new AddNotificationDto
                    {
                        Title = message.Sender.FullName,
                        Content = message.Content,
                        type = NotificationType.MessageReceived,
                        refId = convo.Id
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
