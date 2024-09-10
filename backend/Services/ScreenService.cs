using backend.Data.Models;
using backend.Models;
using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Services;

public class ScreenService : IScreenService
{
    private readonly AppDbContext _context;
    private readonly IHubContext<ScreenHub> _hubContext;

    public ScreenService(AppDbContext context, IHubContext<ScreenHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public IEnumerable<ScreenDTO> GetAllScreens()
    {
        return _context.Screens
            .Select(s => new ScreenDTO
            {
                ScreenId = s.ScreenId,
                ScreenName = s.ScreenName,
                Location = s.Location
            })
            .ToList();
    }

    public ScreenDTO GetScreenById(int screenId)
    {
        var screen = _context.Screens.Find(screenId);
        if (screen == null)
        {
            return null;
        }

        return new ScreenDTO
        {
            ScreenId = screen.ScreenId,
            ScreenName = screen.ScreenName,
            Location = screen.Location
        };
    }

    public ScreenDTO CreateScreen(CreateScreenDTO screenDto)
    {
      
        // Create a new Screen object
        var screen = new Screen
        {
            ScreenName = screenDto.ScreenName,
            Location = screenDto.Location,
            // Generate Barcode URL or code here
            ConnectionCode = GenerateUniqueCode() // Ensure this method generates a valid URL or value
        };

        _context.Screens.Add(screen);
        _context.SaveChanges(); // This should work if BarcodeUrl is correctly set

        return new ScreenDTO
        {
            ScreenId = screen.ScreenId,
            ScreenName = screen.ScreenName,
            Location = screen.Location,
            ConnectionCode = screen.ConnectionCode
        };
    }


    public ScreenDTO UpdateScreen(int screenId, UpdateScreenDTO screenDto)
    {
        var screen = _context.Screens.Find(screenId);
        if (screen == null) throw new KeyNotFoundException("Screen not found");

        if (screenDto.ScreenName != null) screen.ScreenName = screenDto.ScreenName;
        if (screenDto.Location != null) screen.Location = screenDto.Location;

        _context.Screens.Update(screen);
        _context.SaveChanges();

        // Broadcast update to all connected clients
        _hubContext.Clients.All.SendAsync("ReceiveScreenUpdate", screen.ScreenId, "Screen details updated");

        return new ScreenDTO
        {
            ScreenId = screen.ScreenId,
            ScreenName = screen.ScreenName,
            Location = screen.Location
        };
    }

    // public void DeleteScreen(int screenId)
    // {
    //     var screen = _context.Screens.Find(screenId);
    //     if (screen == null) throw new KeyNotFoundException("Screen not found");
    //
    //     _context.Screens.Remove(screen);
    //     _context.SaveChanges();
    // }

    // public void AssignUserToScreen(int screenId, int userId)
    // {
    //     if (!_context.Users.Any(u => u.UserId == userId))
    //         throw new KeyNotFoundException("User not found");
    //
    //     if (!_context.Screens.Any(s => s.ScreenId == screenId))
    //         throw new KeyNotFoundException("Screen not found");
    //
    //     var userScreen = new UserScreen
    //     {
    //         UserId = userId,
    //         ScreenId = screenId
    //     };
    //
    //     _context.UserScreens.Add(userScreen);
    //     _context.SaveChanges();
    // }

    public bool RemoveUserFromScreen(int userId, int screenId)
    {
        // Find the UserScreen entry that matches the userId and screenId
        var userScreen = _context.UserScreens
            .FirstOrDefault(us => us.UserId == userId && us.ScreenId == screenId);

        if (userScreen == null)
        {
            return false; // No entry found; nothing to remove
        }

        // Remove the entry from the UserScreen table
        _context.UserScreens.Remove(userScreen);
        _context.SaveChanges();

        return true; // Successfully removed the user-screen relationship
    }


    // Generate a unique code for the screen
    public string GenerateUniqueCode()
    {
        // Generate a random alphanumeric code
        string code = GenerateRandomCode();

        // Ensure the code is unique (optional, but recommended)
        while (_context.Screens.Any(s => s.ConnectionCode == code))
        {
            code = GenerateRandomCode();
        }


        return code;
    }

    // Helper method to generate a random alphanumeric code
    private string GenerateRandomCode(int length = 6)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Method to validate the code and connect a user to the screen
    public bool ConnectUserToScreen(int userId, string code)
    {
        var screen = _context.Screens.FirstOrDefault(s => s.ConnectionCode == code);
        if (screen == null) return false; // Invalid code

        // Add the connection in UserScreen
        var userScreen = new UserScreen
        {
            UserId = userId,
            ScreenId = screen.ScreenId
        };

        _context.UserScreens.Add(userScreen);
        _context.SaveChanges();

        return true; // Successfully connected
    }
}