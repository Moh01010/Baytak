using Baytak.Application.DTOs.Property;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Baytak.Domain.Enums;
using Baytak.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repo;
        private readonly IBookingRepository _bookRepository;

        public PropertyService(IPropertyRepository repo, IBookingRepository bookRepository)
        {
            _repo = repo;
            _bookRepository = bookRepository;
        }

        public async Task AddAsync(CreatePropertyDto dto, string userId)
        {
            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Area=dto.Area,
                Rooms = dto.Rooms,
                City = dto.City,
                Address = dto.Address,
                Type=dto.PropertyType,
                AgentId = userId
            };
            await _repo.AddAsync(property);
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            var property = await _repo.GetByIdAsync(id);
            if(property == null || property.AgentId != userId)
            {
                throw new Exception("Unauthorized");
            }
            await _repo.DeleteAsync(property);
        }

        public async Task UpdateAsync(Guid id, UpdatePropertyDto dto, string userId)
        {
            var property=await _repo.GetByIdAsync(id);
            if (property == null || property.AgentId != userId)
            {
                throw new Exception("Unauthorized");
            }
            property.Title = dto.Title;
            property.Description = dto.Description;
            property.Price = dto.Price;
            property.Rooms = dto.Rooms;
            property.City = dto.City;
            property.Address = dto.Address;
            property.Type = dto.PropertyType;
            property.Area= dto.Area;

            await _repo.UpdateAsync(property);
        }
        public async Task<IEnumerable<PropertyAllDto>> GetAllAsync()
        {
            var properties=await _repo.GetAllAsync();

            return properties.Select(p => new PropertyAllDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Rooms = p.Rooms,
                City = p.City,
                MainImageUrl = p.Images?
                        .OrderBy(i => i.Id)
                        .FirstOrDefault()?.ImageUrl
            }).ToList();
        }

        public async Task<PropertyDto> GetByIdAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null)
            {
                return null;
            }
            return new PropertyDto
            {
                Id = p.Id,
                AgentId= p.AgentId,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Type=p.Type,
                Rooms = p.Rooms,
                City = p.City,
                Address = p.Address,
                Status= p.Status,
                Area= p.Area,
                ImageUrls = p.Images?
                     .Where(i => !i.IsDeleted) 
                     .Select(i => i.ImageUrl)
                     .ToList() ?? new List<string>()
            };
        }
        public async Task MarkAsSoldAsync(Guid id, string userId)
        {
            var property = await _repo.GetByIdAsync(id);

            if (property == null)
                throw new Exception("Property not found");

            if (property.AgentId != userId)
                throw new UnauthorizedAccessException();

            property.Status = PropertyStatus.Sold;
            await _repo.UpdateAsync(property);

            var bookings = await _bookRepository.GetByPropertyIdAsync(property.Id);

            foreach (var b in bookings.Where(b => b.Status == BookingStatus.Pending))
            {
                b.Status = BookingStatus.Cancelled;
            }
            await _bookRepository.UpdateRangeAsync(bookings);

        }

    }
}