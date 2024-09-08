using backend.Models;
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<dtoContent>>> GetUserContents(int userId)
        {
            var contents = await _contentServices.GetUserContentsAsync(userId);
        
          
        
       return Ok(contents);
         }

        [HttpPost]
        public async Task<ActionResult<dtoContent>> AddContent([FromForm] dtoContent contentDto)
        {
            // Map ContentDto to Content model
            // var content = new dtoContent
            // {
            //     UserId = contentDto.UserId,
            //     Type = contentDto.Type,
            //     Title = contentDto.Title,
            //     Description = contentDto.Description,
            //     StartTime = contentDto.StartTime,
            //     EndTime = contentDto.EndTime,
            //     CreatedAt = DateTime.UtcNow,
            //     UpdatedAt = DateTime.UtcNow,
            //     File = file  // Handle file separately
            // };
            //
            // var addedContent = await _contentServices.AddContentAsync(content);
            //
            // // Map back to DTO
            // var addedContentDto = new dtoContent
            // {
            //     ContentId = addedContent.ContentId,
            //     UserId = addedContent.UserId,
            //     Type = addedContent.Type,
            //     Title = addedContent.Title,
            //     Description = addedContent.Description,
            //     StartTime = addedContent.StartTime,
            //     EndTime = addedContent.EndTime,
            //     FilePath = addedContent.FilePath
            // };
            //
            // return CreatedAtAction(nameof(GetUserContents), new { userId = addedContentDto.UserId }, addedContentDto);
            if (ModelState.IsValid)
            {
            var contentResponse = await _contentServices.AddContentAsync(contentDto);
            return Ok(contentResponse);
            }
            return BadRequest();
        }

        [HttpPut("{contentId}")]
        public async Task<ActionResult<dtoContent>> UpdateContent(int contentId, [FromForm] dtoContent contentDto)
        {
            if (contentDto == null)
            {
                return BadRequest("Content data is required.");
            }
            var updatedContent = await _contentServices.UpdateContentAsync(contentId, contentDto);

            if (updatedContent == null)
            {
                return NotFound($"Content with ID {contentId} not found.");
            }

            return Ok(updatedContent);
        }

         [HttpDelete("{contentId}")]
        public async Task<ActionResult<dtoContent>> DeleteContent(int contentId)
        {
            var deletedContent = await _contentServices.RemoveContentAsync(contentId);
        
            if (deletedContent == null)
            {
                return NotFound();
            }
        
            return Ok(deletedContent);
        }
    }
}