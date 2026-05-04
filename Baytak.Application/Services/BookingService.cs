using Baytak.Application.DTOs.Booking;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace Baytak.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IPropertyRepository _propertyRepo;

        public BookingService(IBookingRepository repo, IPropertyRepository propertyRepo)
        {
            _repo = repo;
            _propertyRepo = propertyRepo;
        }

        public async Task AddAsync(AddBookingDto dto, string UserId)
        {

            var property = await _propertyRepo.GetByIdAsync(dto.PropertyId);
            if (property == null)
                throw new Exception("Property not found");

            if (property.Status == PropertyStatus.Sold)
                throw new Exception("This property is already sold");

            if (property.AgentId == UserId)
                throw new Exception("You cannot book your own property");

            var countVisits=await _repo.NumberOfVisitsAsync(UserId,dto.PropertyId);
            if(countVisits>=2)
                throw new Exception("You have reached the maximum number of visits for this property");

            var hasPending = await _repo.HasPendingBookingAsync(UserId, dto.PropertyId,dto.BookingDate);

            if (hasPending)
                throw new Exception("You already have a pending booking for this property");

            if (dto.StartTime >= dto.EndTime)
                throw new Exception("Invalid time range");

            if ((dto.EndTime - dto.StartTime).TotalHours > 2)
                throw new Exception("Max booking is 2 hours");

            var isBooked = await _repo.IsTimeBooked(dto.PropertyId, dto.BookingDate,dto.StartTime,dto.EndTime);

            if (isBooked)
                throw new Exception("This time is not available");

            var Booking = new Booking
            {
                UserId = UserId,
                PropertyId = dto.PropertyId,
                AgentId = property.AgentId,
                BookingDate = dto.BookingDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = BookingStatus.Pending,
                CreatedAt=DateTime.UtcNow
            };

            await _repo.AddAsync(Booking);

        }

        public async Task ApproveAsync(Guid Id,string UserId)
        {
            var booking=await _repo.GetByIdAndAgentAsync(Id,UserId);

            if(booking == null)
                throw new Exception($"{nameof(Booking)} was not found");

            if (booking.Status != BookingStatus.Pending)
                throw new Exception("Booking already processed");

            booking.Status = BookingStatus.Approved;

            await _repo.UpdateAsync(booking);

            var otherBookings = await _repo.GetOverlappingBookingsAsync(
                                                booking.PropertyId,
                                                booking.BookingDate,
                                                booking.StartTime,
                                                booking.EndTime,
                                                booking.Id);

            foreach (var item in otherBookings)
            {
                item.Status = BookingStatus.Rejected;
            }

            await _repo.UpdateRangeAsync(otherBookings);

        }

        public async Task DeleteAsync(Guid Id,string UserId)
        {
            var booking = await _repo.GetBookingByIdAsync(Id);
            if (booking == null)
                throw new Exception("Bad request");
            if(UserId==booking.UserId)
            {
                booking.Status = BookingStatus.Cancelled;
                await _repo.UpdateAsync(booking);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<IEnumerable<BookingAgentAllDto>> GetAgentBookingsAsync(string agentId, BookingStatus? status)
        {
            var bookings = await _repo.GetBookingsByAgentIdAsync(agentId,status);

            return bookings.Select(b => new BookingAgentAllDto
            {
                Id = b.Id,
                BookingDate = b.BookingDate,
                Status = b.Status,
                CreatedAt = b.CreatedAt,
                PropertyTitle = b.Property.Title,
                UserName = b.User.FullName
            });
        }

        public async Task<IEnumerable<BookingAllDto>> GetAsync(string UserId)
        {
            var result = await _repo.GetUserBookingsAsync(UserId);

            var bookingUser = result.Select(b => new BookingAllDto
            {
                Id = b.Id,
                BookingDate = b.BookingDate,
                Status = b.Status,
                CreatedAt = b.CreatedAt,
                PropertyTitle = b.Property.Title
            });

            return bookingUser;
        }

        public async Task RejectAsync(Guid id, string userId)
        {
            var booking = await _repo.GetByIdAndAgentAsync(id, userId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.Status != BookingStatus.Pending)
                throw new Exception("Booking already processed");

            booking.Status = BookingStatus.Rejected;

            await _repo.UpdateAsync(booking);
        }
    }
}