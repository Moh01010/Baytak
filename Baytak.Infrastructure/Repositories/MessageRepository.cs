using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetByConversationIdAsync(Guid conversationId)
        {
            return await _context.Messages
                .Where(m=>m.ConversationId==conversationId)
                .OrderBy(m=>m.CreatedAt)
                .ToListAsync();
        }
        public async Task MarkAsSeenAsync(Guid conversationId, string userId)
        {
            var messages = await _context.Messages
                .Where(m => m.ConversationId == conversationId
                         && m.SenderId != userId
                         && !m.IsSeen)
                .ToListAsync();

            foreach (var msg in messages)
            {
                msg.IsSeen = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
