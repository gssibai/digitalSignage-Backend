using backend.Models;
using DigitalSignageApi.Models;

namespace backend.Interfaces
{
    public interface IUserService
    {

        Task<UserDto> RegisterUserAsync(NewUserDto userDto);
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
            Task<bool> DeleteUserAsync(int userId);
            Task<bool> UpdateUserAsync(User user);
         
        
    }
}