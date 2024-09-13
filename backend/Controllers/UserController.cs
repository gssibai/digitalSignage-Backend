using backend.Interfaces;
using backend.Models;
using backend.Services;
using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
       private readonly IUserService _userService;

       public UserController(IUserService userService)
       {
           _userService = userService;
       }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> RegisterNewUser(NewUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var registerResponse = await _userService.RegisterUserAsync(userDto);
            return Ok(registerResponse);
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
                var token = await _userService.LoginUserAsync(loginUserDto);
                if(token == "Invalid credentials.") return Unauthorized("Invalid credentials");
                return Ok(new { Token = token });
        }
        
        [HttpPut("id")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updateResponse = await _userService.UpdateUserAsync(id, updateUserDto);
            return Ok(updateResponse);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var deleteResponse = await _userService.DeleteUserAsync(id);
            if (deleteResponse) return Ok(deleteResponse);
            return BadRequest("User not found");
        }
        
        [HttpPut("{id}/ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto == null || string.IsNullOrWhiteSpace(resetPasswordDto.NewPassword))
            {
                return BadRequest("New password is required.");
            }

           
                var result = await _userService.ResetPasswordAsync(id, resetPasswordDto.NewPassword);
                if (result)
                {
                    return Ok("Password has been reset successfully.");
                }

                return NotFound("User not found.");
            }
    }
}
