using backend.Models;
using DigitalSignageApi.Models;

namespace backend.Interfaces
{
    public interface IUserService
    {

        Task<UserDto> RegisterUserAsync(NewUserDto userDto);
            Task<bool> DeleteUserAsync(int userId);
            Task<User> GetUserByIdAsync(int userId);
            Task<bool> UpdateUserAsync(User user);
         
        
    }
}