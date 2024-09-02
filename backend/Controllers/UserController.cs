// using backend.Interfaces;
// using backend.Services;
// using DigitalSignageApi.Data;
// using DigitalSignageApi.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
//
// namespace backend.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class UserController : ControllerBase
//     {
//         private readonly IUserService _userService;
//
//         public UserController(IUserService userService)
//         {
//             _userService = userService;
//         }
//
//         // Login
//         [AllowAnonymous]
//         [HttpPost("login")]
//         public async Task<IActionResult> Login([FromBody] LoginServices login)
//         {
//             var user = await _userService.AuthenticateAsync(login.Email, login.Password);
//
//             if (user == null)
//                 return Unauthorized(new { message = "Invalid username or password" });
//
//             var token = _jwtService.GenerateToken(user.UserId, user.Role);
//             return Ok(new { token });
//         }
//
//         // Create a new user
//         [HttpPost("register")]
//         [AllowAnonymous]
//         public async Task<IActionResult> Register(User user)
//         {
//             var createdUser = await _userService.RegisterUserAsync(user);
//             return Ok(createdUser);
//         }
//
//         // Delete an existing user
//         [HttpDelete("{id}")]
//         [Authorize]
//         public async Task<IActionResult> DeleteAccount(int id)
//         {
//             var result = await _userService.DeleteUserAsync(id);
//             if (result) return Ok();
//             return NotFound();
//         }
//
//         // Get user profile information
//         [HttpGet("{id}")]
//         [Authorize]
//         public async Task<IActionResult> GetUserInfo(int id)
//         {
//             var user = await _userService.GetUserByIdAsync(id);
//             if (user != null) return Ok(user);
//             return NotFound();
//         }
//
//         // Edit user profile information
//         [HttpPut("{id}")]
//         [Authorize]
//         public async Task<IActionResult> EditUserInfo(int id, User user)
//         {
//             user.UserId = id;
//             var result = await _userService.UpdateUserAsync(user);
//             if (result) return Ok();
//             return BadRequest();
//         }
//
//       
//     }
// }
