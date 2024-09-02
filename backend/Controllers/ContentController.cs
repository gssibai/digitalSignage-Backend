using backend.Services;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly ContentServices _contentServices;

        public ContentController(ContentServices contentServices)
        {
            _contentServices = contentServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddContent(Content content)
        {
            var newContent = await _contentServices.AddContentAsync(content);
            return Ok(newContent);
        }
        
        [HttpDelete("{contentId}")]
        public async Task<IActionResult> RemoveContent(int contentId)
        {
            var removedContent = await _contentServices.RemoveContentAsync(contentId);
            if (removedContent == null)
            {
                return NotFound();
            }
            return Ok(removedContent);
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateContent(int contentId, Content content)
        {
            var updatedContent = await _contentServices.UpdateContentAsync(contentId, content);
            if (updatedContent == null)
            {
                return NotFound();
            }
            return Ok(updatedContent);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserContents(int userId)
        {
            var contents = await _contentServices.GetUserContentsAsync(userId);
            return Ok(contents);
        }
    }
}