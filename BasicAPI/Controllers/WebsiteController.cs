using BasicAPI.Models;
using BasicAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly IWebsiteService websiteService;

        public WebsiteController(IWebsiteService websiteService)
        {
            this.websiteService = websiteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await websiteService.GetAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] QueryDescriptor filter)
        {
            var result = await websiteService.GetAsync(filter);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] WebsiteAddModel model)
        {
            Guid id = await websiteService.AddAsync(model);
            return Created(nameof(PostAsync), new { id });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(Guid id, [FromBody] WebsiteUpdateModel model)
        {
            var updated = await websiteService.UpdateAsync(id, model);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await websiteService.DeleteAsync(id);
            return NoContent();
        }
    }
}