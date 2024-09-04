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

        // [HttpGet("{userId}")]
        // public async Task<ActionResult<IEnumerable<dtoContent>>> GetUserContents(int userId)
        // {
        //     var contents = await _contentServices.GetUserContentsAsync(userId);
        //
        //     // Map Content model to ContentDto
        //     // var contentDtos = contents.Select(c => new dtoContent
        //     // {
        //     //     ContentId = c.ContentId,
        //     //     UserId = c.UserId,
        //     //     Type = c.Type,
        //     //     Title = c.Title,
        //     //     Description = c.Description,
        //     //     StartTime = c.StartTime,
        //     //     EndTime = c.EndTime,
        //     //     CreatedAt = c.CreatedAt,
        //     //     UpdatedAt = c.UpdatedAt,
        //     //     FilePath = c.FilePath
        //     // });
        //
        //     // return Ok(contentDtos);
        // }

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

        // [HttpPut("{contentId}")]
        // public async Task<ActionResult<dtoContent>> UpdateContent(int contentId, [FromForm] dtoContent contentDto, [FromForm] IFormFile? file)
        // {
        //     // Map ContentDto to Content model
        //     var content = new Content
        //     {
        //         ContentId = contentId,
        //         UserId = contentDto.UserId,
        //         Type = contentDto.Type,
        //         Title = contentDto.Title,
        //         Description = contentDto.Description,
        //         StartTime = contentDto.StartTime,
        //         EndTime = contentDto.EndTime,
        //         File = file // Handle file separately
        //     };
        //
        //     var updatedContent = await _contentServices.UpdateContentAsync(contentId, content);
        //
        //     if (updatedContent == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     // Map back to DTO
        //     var updatedContentDto = new dtoContent
        //     {
        //         ContentId = updatedContent.ContentId,
        //         UserId = updatedContent.UserId,
        //         Type = updatedContent.Type,
        //         Title = updatedContent.Title,
        //         Description = updatedContent.Description,
        //         StartTime = updatedContent.StartTime,
        //         EndTime = updatedContent.EndTime,
        //         CreatedAt = updatedContent.CreatedAt,
        //         UpdatedAt = updatedContent.UpdatedAt,
        //         FilePath = updatedContent.FilePath
        //     };
        //
        //     return Ok(updatedContentDto);
        // }

        // [HttpDelete("{contentId}")]
        // public async Task<ActionResult<dtoContent>> DeleteContent(int contentId)
        // {
        //     var deletedContent = await _contentServices.RemoveContentAsync(contentId);
        //
        //     if (deletedContent == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     // Map deleted Content to DTO
        //     var deletedContentDto = new dtoContent
        //     {
        //         ContentId = deletedContent.ContentId,
        //         UserId = deletedContent.UserId,
        //         Type = deletedContent.Type,
        //         Title = deletedContent.Title,
        //         Description = deletedContent.Description,
        //         StartTime = deletedContent.StartTime,
        //         EndTime = deletedContent.EndTime,
        //         CreatedAt = deletedContent.CreatedAt,
        //         UpdatedAt = deletedContent.UpdatedAt,
        //         FilePath = deletedContent.FilePath
        //     };
        //
        //     return Ok(deletedContentDto);
        // }
    }
}