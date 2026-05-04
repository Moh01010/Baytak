using Baytak.Application.DTOs.Property;
using Baytak.Application.Interfaces;
using Baytak.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Baytak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _service;
        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto dto)
        {

            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            await _service.AddAsync(dto,userId);
            return Ok(new { message = "Property created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,UpdatePropertyDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _service.UpdateAsync(id, dto, userId);
                return Ok(new { message = "Property updated successfully" });
            }
            catch (Exception ex) when (ex.Message == "Unauthorized")
            {
                return Forbid(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _service.DeleteAsync(id,userId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound(); 
            return Ok(result);
        }
        [HttpPut("{id}/mark-as-sold")]
        public async Task<IActionResult> MarkAsSold(Guid id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                await _service.MarkAsSoldAsync(id, userId);

                return Ok(new { message = "Property marked as sold successfully" });
            }
            catch (Exception ex) when (ex.Message == "Unauthorized")
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
