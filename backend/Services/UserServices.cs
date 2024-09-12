using backend.Interfaces;
using backend.Models;
using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserServices : IUserService
{
        private readonly AppDbContext _context;


        public UserServices(AppDbContext context)
        {
            _context = context;
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

        public Task<bool> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
}
    