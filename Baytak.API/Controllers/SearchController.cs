using Baytak.Application.DTOs.Property;
using Baytak.Application.Interfaces;
using Baytak.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Baytak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _service;

        public SearchController(ISearchService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]PropertySearchDto filter)
        {
            var results=await _service.SearchPropertiesAsync(filter);
            return Ok(results);
        }
    }
}
