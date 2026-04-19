
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
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _service;

        public FavoriteController(IFavoriteService service)
        {
            _service = service;
        }
        [HttpPost("{PropertyId}")]
        public async Task<IActionResult> Add(Guid PropertyId)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null ) 
                return Unauthorized();

            await _service.AddAsync(PropertyId, userId);

            return Ok("Property Add to Favorite successfully");
        }
        [HttpDelete("{PropertyId}")]
        public async Task<IActionResult> Delete(Guid PropertyId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                    return Unauthorized();

                await _service.DeleteAsync(PropertyId, userId);
                return Ok("Property Delete to Favorite successfully");
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var result=await _service.GetAsync(userId);

            return Ok(result);
        }
    }
}
