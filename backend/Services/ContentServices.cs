using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class ContentServices
{
    private readonly AppDbContext _context;

    public ContentServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Content> AddContentAsync([FromForm] Content content)
    {
        // Ensure the UserId is set
        if (content.UserId <= 0)
        {
            throw new ArgumentException("UserId is required.");
        }

        // Define the path to save the file in a user-specific directory
        var userFolder = Path.Combine("uploads", content.UserId.ToString());
        Directory.CreateDirectory(userFolder); // Ensure the user directory exists

        // Generate a unique file name
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(content.File.FileName);
        var filePath = Path.Combine(userFolder, uniqueFileName);

        // Save the file to the specified path
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await content.File.CopyToAsync(fileStream);
        }

        // Set the file path and other content properties
        content.FilePath = filePath;
        content.CreatedAt = DateTime.UtcNow;
        content.UpdatedAt = DateTime.UtcNow;

        await _context.Contents.AddAsync(content);
        await _context.SaveChangesAsync();
        return content;
    }

    public async Task<Content> RemoveContentAsync(int contentId)
    {
        var content = await _context.Contents.FindAsync(contentId);
        if (content == null)
        {
            return null;
        }

        // Delete the file from the file system
        if (File.Exists(content.FilePath))
        {
            File.Delete(content.FilePath);
        }

        _context.Contents.Remove(content);
        await _context.SaveChangesAsync();
        return content;
    }

    public async Task<IEnumerable<Content>> GetUserContentsAsync(int userId)
    {
        return await _context.Contents
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Content> UpdateContentAsync(int contentId, Content content)
    {
        var _content = await _context.Contents.FindAsync(contentId);
        if (_content == null)
        {
            return null;
        }

        // Delete the existing file if a new file is uploaded
        if (content.File != null)
        {
            if (File.Exists(_content.FilePath))
            {
                File.Delete(_content.FilePath);
            }

            // Generate a unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(content.File.FileName);
            var filePath = Path.Combine("uploads", content.UserId.ToString(), uniqueFileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await content.File.CopyToAsync(fileStream);
            }

            _content.FilePath = filePath;
        }

        _content.Title = content.Title;
        _content.Type = content.Type;
        _content.Description = content.Description;
        _content.StartTime = content.StartTime;
        _content.EndTime = content.EndTime;
        _content.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return _content;
    }
}
