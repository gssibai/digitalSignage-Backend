using Microsoft.AspNetCore.SignalR;

public class ScreenHub : Hub
{
    // You can override OnConnectedAsync to handle client connections if needed
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    // Method to handle disconnections if needed
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        return base.OnDisconnectedAsync(exception);
    }

    // Broadcast message to all connected clients
    public async Task SendScreenUpdate(int screenId, string message)
    {
        await Clients.All.SendAsync("ReceiveScreenUpdate", screenId, message);
    }
}