using Baytak.Application.DTOs.Property;
using Baytak.Application.Interfaces;
using Baytak.Domain.Interfaces;
using Baytak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repo;
        public PropertyService(IPropertyRepository repo)
        {
            _repo = repo;
        }
        public async Task AddAsync(CreatePropertyDto dto, string userId)
        {
            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Bedrooms = dto.Bedrooms,
                Bathrooms = dto.Bathrooms,
                City = dto.City,
                Address = dto.Address,
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
            property.Bedrooms = dto.Bedrooms;
            property.Bathrooms = dto.Bathrooms;
            property.City = dto.City;
            property.Address = dto.Address;

            await _repo.UpdateAsync(property);
        }
        public async Task<IEnumerable<PropertyDto>> GetAllAsync()
        {
            var properties=await _repo.GetAllAsync();

            return properties.Select(p => new PropertyDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Bedrooms = p.Bedrooms,
                City = p.City
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
                Title = p.Title,
                Price = p.Price,
                Bedrooms = p.Bedrooms,
                City = p.City
            };
        }

    }
}
