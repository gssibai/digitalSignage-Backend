using backend.Models;
using backend.Services;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
  //  [Authorize]
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
            if (ModelState.IsValid)
            {
                var contentResponse = await _contentServices.AddContentAsync(contentDto);
                return Ok(contentResponse);
            }

            return BadRequest();
        }

       [HttpPost("assign")]
       public async Task<IActionResult> AssignAssets([FromBody] AssignAsstetsDto request)
       {
           if (request.ContentIds == null || !request.ContentIds.Any())
           {
               return BadRequest("No content ids provided");
           }

           var assignedContentIds = await _contentServices.AssignAssetsAsync(request);
           return Ok( new {AssignedContentIds =  assignedContentIds});
       }

       [HttpGet("assigned-contents/{screenId}")]
       public async Task<IActionResult> GetAssignedContents(int screenId)
       {
           var contentsId = _contentServices.GetAssignedContentIdsAsync(screenId);
           if (!contentsId.Result.Any())
           {
               return NotFound("No content ids provided");
           }
           return Ok(contentsId);
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