using Baytak.Application.DTOs.Booking;
using Baytak.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface IBookingService
    {
        Task AddAsync(AddBookingDto dto,string UserId);
        Task DeleteAsync(Guid Id,string UserId);
        Task<IEnumerable<BookingAllDto>> GetAsync(string UserId);
        Task<IEnumerable<BookingAgentAllDto>> GetAgentBookingsAsync(string agentId, BookingStatus? status);
        Task ApproveAsync(Guid Id, string UserId);
        Task RejectAsync(Guid id, string userId);
    }
}
