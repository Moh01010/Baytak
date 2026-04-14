using Baytak.Application.DTOs.PropertyImage;
using Baytak.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Baytak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyImageController : ControllerBase
    {
        private readonly IPropertyImageService _service;

        public PropertyImageController(IPropertyImageService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UploadImageDto dto)
        {
            if (dto.file == null || dto.file.Length == 0)
                return BadRequest("Please upload an image.");

            var imageUrl=await _service.UploadAsync(dto);

            return Ok(new { imageUrl });
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetImageDto dto)
        {
            var images=await _service.GetImagesAsync(dto);
            return Ok(images);
        }
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> Delete(Guid imageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            

            await _service.DeleteAsync(imageId,userId);

            return Ok("Deleted");
        }
    }
}
