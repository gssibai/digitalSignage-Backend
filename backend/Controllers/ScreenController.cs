using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class ScreenController : ControllerBase
    {
        private readonly IScreenService _screenService;

        public ScreenController(IScreenService screenService)
        {
            _screenService = screenService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<ScreenDTO>> GetScreens()
        {
            var allScreens = _screenService.GetAllScreens();
            if (ModelState.IsValid)
            {
                return Ok(allScreens);
            }

            return BadRequest(allScreens);
        }
        
        [HttpGet("connection/{screenId}")]
        public ActionResult<String> GetConnectionCode(int screenId)
        {
            var screen = _screenService.GetConnectionCode(screenId);
            if(!ModelState.IsValid)
                return BadRequest(screen);
            return screen;
        }
        
        
        [HttpGet("userId")]
        public ActionResult<IEnumerable<ScreenDTO>> GetUserScreens(int userId)
        {
            var allScreens = _screenService.GetAllScreensByUserId(userId);
            if (ModelState.IsValid)
            {
                return Ok(allScreens);
            }

            return BadRequest(allScreens);
        }
        
        [HttpGet("{screenId}")]
        public ActionResult<ScreenDTO> GetScreen(int screenId)
        {
            var getById = _screenService.GetScreenById(screenId);
            if (getById != null)
            {
            return Ok(getById);
            }
            return NotFound(getById);
        }

        [HttpPost]
        public ActionResult<ScreenDTO> CreateScreen([FromBody] CreateScreenDTO screenDto)
        {
            var createdScreen = _screenService.CreateScreen(screenDto);
          
            return CreatedAtAction(nameof(GetScreen), new { screenId = createdScreen.ScreenId }, createdScreen);
        }


        [HttpPut("{screenId}")]
        public ActionResult<ScreenDTO> UpdateScreen(int screenId, [FromBody] UpdateScreenDTO screenDto)
        {
            return Ok(_screenService.UpdateScreen(screenId, screenDto));
        }
        
        [HttpDelete("{screenId}/remove-user/{userId}")]
        public IActionResult RemoveUserFromScreen(int screenId, int userId)
        {
            var removeScreen = _screenService.RemoveUserFromScreen(userId, screenId);
            if (removeScreen)
            {
                return Ok(removeScreen);
            }
            else
            {
                return NotFound(removeScreen);
            }
        }

    
        [HttpPost("connect")]
        public IActionResult ConnectUserToScreen([FromBody] ConnectUserRequest request)
        {
            var connection = _screenService.ConnectUserToScreen(request.UserId, request.Code);
            if (connection)
            {
                return Ok(connection);
            }
            else
            {
                return BadRequest("Invalid code or user not found.");
            }
        }
    }
}