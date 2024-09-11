using backend.Interfaces;
using backend.Models;
using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserService 
    {
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        private readonly UserManager<User> _userManager;

        public async Task<UserDto> RegisterUserAsync(NewUserDto userDto)
        {
           // user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
           var user = new User
           {
               Email = userDto.Email,
               Name = userDto.Name,
               Surname = userDto.Surname,
              // PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
              PasswordHash = userDto.Password,
              Role = "User"
           };
           IdentityResult registerResult = await _userManager.CreateAsync(user, userDto.Password);

           return new UserDto()
           {
               Email = user.Email,
               Name = user.Name,
               Surname = user.Surname,
               Password = user.PasswordHash,
               UserId = user.UserId
           };
        }
      

       
    }
    