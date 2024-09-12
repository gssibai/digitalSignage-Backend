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


        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterNewUser(NewUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var registerResponse = await _userService.RegisterUserAsync(userDto);
            return Ok(registerResponse);
        }
    }
}
