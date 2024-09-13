using backend.Models;

namespace backend.Services;

public interface IScreenService
{
    IEnumerable<ScreenDTO> GetAllScreens();
    ScreenDTO GetScreenById(int screenId);
    ScreenDTO CreateScreen(CreateScreenDTO screenDto);
    ScreenDTO UpdateScreen(int screenId, UpdateScreenDTO screenDto);
  //  void DeleteScreen(int screenId);
   // void AssignUserToScreen(int screenId, int userId);
    bool RemoveUserFromScreen(int screenId, int userId);
    string GenerateUniqueCode();
    bool ConnectUserToScreen(int userId, string code);
    IEnumerable<ScreenDTO> GetAllScreensByUserId(int userId);
}