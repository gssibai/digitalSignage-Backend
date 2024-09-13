using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Interfaces;
using backend.Models;
using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class UserServices : IUserService
{
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDto> RegisterUserAsync(NewUserDto userDto)
        {
           // user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
           try
           {
               var user = new User
               {
                   Email = userDto.Email,
                   Name = userDto.Name,
                   Surname = userDto.Surname,
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                  // PasswordHash = userDto.Password,
                   Role = "User"
               };
               await _context.Users.AddAsync(user);
               await _context.SaveChangesAsync();
               return new UserDto()
               {
                   Email = user.Email,
                   Name = user.Name,
                   Surname = user.Surname,
                   UserId = user.UserId
               };
           }
           catch (Exception ex)
           {
               throw new ApplicationException("An error occurred while registering the user.", ex);

           }
        }
        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            // Find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);
            if (user == null)
            {
               return ("Invalid credentials.");
            }

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
            {
                return("Invalid credentials.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return false;
             _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

       

            public async Task<UserDto> UpdateUserAsync(int UserId, UpdateUserDto userDto)
            {
                var user = await _context.Users.FindAsync(UserId);
                if(user == null)
                     throw new ApplicationException("User not found.");
                
                user.Email = userDto.Email ?? user.Email;
                user.Name = userDto.Name ?? user.Name;
                user.Surname = userDto.Surname ?? user.Surname;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password) ?? user.PasswordHash;
                user.UpdatedAt = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new UserDto()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname
                };
            }
            
            public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new ApplicationException("User not found.");
                }

                // Hash the new password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.UpdatedAt = DateTime.Now;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return true;
            }
}
    