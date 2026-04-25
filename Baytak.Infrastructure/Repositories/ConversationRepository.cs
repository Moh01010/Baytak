using Baytak.Domain.Entities;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly AppDbContext _context;

        public ConversationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Conversation conversation)
        {
            await _context.AddAsync(conversation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Conversation?> GetAsync(string userId, string agentId)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c =>
                c.UserId == userId && c.AgentId == agentId);
        }
        public async Task<Conversation?> GetByIdAsync(Guid id)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Conversation>> GetUserConversationsAsync(string userId)
        {
            return await _context.Conversations
                .Where(c => c.UserId == userId || c.AgentId == userId)
                .Include(c => c.Messages)
                .ToListAsync();
        }
    }
}
