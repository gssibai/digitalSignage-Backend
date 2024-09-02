using DigitalSignageApi.Models;

namespace backend.Interfaces
{
    public interface IUserService
    {
        
            Task<User> RegisterUserAsync(User user);
            Task<bool> DeleteUserAsync(int userId);
            Task<User> GetUserByIdAsync(int userId);
            Task<bool> UpdateUserAsync(User user);
         
        
    }
}