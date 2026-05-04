using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using Baytak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAndAgentAsync(Guid id,string Id)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b=>b.AgentId==Id&&b.Id==id);
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string Id)
        {
            return await _context.Bookings
                .Include(b=>b.Property)
                .Where(b => b.UserId == Id).ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid Id)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task<bool> IsTimeBooked(Guid propertyId, DateTime date, TimeSpan start, TimeSpan end)
        {
            return await _context.Bookings
         .AnyAsync(b => b.PropertyId == propertyId
             && b.BookingDate.Date == date.Date
             && b.Status == BookingStatus.Approved
             && (
                 start < b.EndTime && end > b.StartTime
             ));
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Booking>> GetOverlappingBookingsAsync(Guid propertyId, DateTime date, TimeSpan start, TimeSpan end, Guid excludeId)
        {
            return await _context.Bookings
        .Where(b => b.PropertyId == propertyId
                 && b.BookingDate.Date == date.Date
                 && b.Id != excludeId
                 && b.Status == BookingStatus.Pending
                 && (
                     start < b.EndTime && end > b.StartTime
                 ))
        .ToListAsync();
        }
        public async Task UpdateRangeAsync(List<Booking> bookings)
        {
            _context.Bookings.UpdateRange(bookings);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByAgentIdAsync(string agentId, BookingStatus? status)
        {
            var query = _context.Bookings
        .Include(b => b.Property)
        .Include(b => b.User)
        .Where(b => b.AgentId == agentId);

            if (status.HasValue)
                query = query.Where(b => b.Status == status.Value);

            return await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
        public async Task<bool> HasPendingBookingAsync(string userId, Guid propertyId,DateTime date)
        {
            return await _context.Bookings
                .AnyAsync(b => b.UserId == userId
            && b.PropertyId == propertyId
            && b.BookingDate.Date == date.Date
            && b.Status == BookingStatus.Pending);
        }

        public async Task<int> NumberOfVisitsAsync(string userId,Guid propertyId)
        {
            var count = await _context.Bookings
                .CountAsync(b => b.UserId == userId
                    && b.PropertyId == propertyId
                    && b.Status == BookingStatus.Approved);
            return count;
        }

        public async Task<List<Booking>> GetByPropertyIdAsync(Guid propertyId)
        {
            return await _context.Bookings
                .Where(b => b.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}
