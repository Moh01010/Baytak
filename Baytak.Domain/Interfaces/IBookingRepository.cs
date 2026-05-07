using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task DeleteAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(Guid Id);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string Id);
        Task<Booking?> GetByIdAndAgentAsync(Guid id, string Id);
        Task<IEnumerable<Booking>> GetBookingsByAgentIdAsync(string agentId, BookingStatus? status);
        Task<bool> IsTimeBooked(Guid propertyId, DateTime date, TimeSpan start, TimeSpan end);
        Task<List<Booking>> GetOverlappingBookingsAsync(Guid propertyId, DateTime date, TimeSpan start,TimeSpan end, Guid excludeId);
        Task<List<Booking>> GetByPropertyIdAsync(Guid propertyId);
        Task UpdateRangeAsync(List<Booking> bookings);
        Task<bool> HasPendingBookingAsync(string userId, Guid propertyId, DateTime date);
        Task<int> NumberOfVisitsAsync(string userId,Guid propertyId);
    }
}
