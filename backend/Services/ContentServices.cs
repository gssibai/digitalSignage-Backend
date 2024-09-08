using backend.Models;
using DigitalSignageApi.Data;
using DigitalSignageApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp; // Include logging namespace

namespace backend.Services;

public class ContentServices
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ContentServices> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public ContentServices(AppDbContext context, IWebHostEnvironment environment, ILogger<ContentServices> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _environment = environment;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<ContentResponseDto> AddContentAsync([FromForm] dtoContent contentDto)
    {
        // Ensure the UserId is set
        if (contentDto.UserId < 0)
        {
            _logger.LogError("UserId is not set.");
            throw new ArgumentException("UserId is required.");
        }

        // Ensure the file is provided and log its details
        if (contentDto.File == null || contentDto.File.Length == 0)
        {
            _logger.LogError("File is null or empty.");
            throw new ArgumentException("File is required and cannot be empty.");
        }

        var content = new Content
        {
            UserId = contentDto.UserId,
            Title = contentDto.Title,
            Type = contentDto.Type,
            Description = contentDto.Description,
            StartTime = contentDto.StartTime,
            EndTime = contentDto.EndTime,
        };

        try
        {
            // Upload the file and get the public URL
            content.FilePath = await UploadFileAsync(contentDto.File, content.UserId);

            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();

            return new ContentResponseDto
            {
              //  ContentId = content.ContentId,
                UserId = content.UserId,
                Title = content.Title,
                Type = content.Type,
                Description = content.Description,
                StartTime = content.StartTime,
                EndTime = content.EndTime,
               // CreatedAt = content.CreatedAt,
                 UpdatedAt = content.UpdatedAt,
                FilePath = content.FilePath
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving the content.");
            throw;
        }
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

    public async Task<ContentResponseDto> UpdateContentAsync(int contentId, [FromForm] dtoContent contentDto)
    {
        var content = await _context.Contents.FindAsync(contentId);
        if (content == null)
        {
            _logger.LogWarning($"Content with ID {contentId} not found.");
            return null;
        }

        // Update content properties if they are not null
        content.Title = contentDto.Title ?? content.Title;
        content.Type = contentDto.Type ?? content.Type;
        content.Description = contentDto.Description ?? content.Description;
        content.StartTime = contentDto.StartTime ?? content.StartTime;
        content.EndTime = contentDto.EndTime ?? content.EndTime;

        // If a new file is uploaded, delete the old one and save the new one
        if (contentDto.File != null && contentDto.File.Length > 0)
        {
            // Delete the old file if exists
            if (!string.IsNullOrWhiteSpace(content.FilePath) && File.Exists(content.FilePath))
            {
                File.Delete(content.FilePath);
            }

            // Upload the new file and get the public URL
            content.FilePath = await UploadFileAsync(contentDto.File, content.UserId);
        }

        content.UpdatedAt = DateTime.UtcNow;

        _context.Contents.Update(content);
        await _context.SaveChangesAsync();

        return new ContentResponseDto
        {
            ContentId = content.ContentId,
            UserId = content.UserId,
            Title = content.Title,
            Type = content.Type,
            Description = content.Description,
            StartTime = content.StartTime,
            EndTime = content.EndTime,
            CreatedAt = content.CreatedAt,
            UpdatedAt = content.UpdatedAt,
            FilePath = content.FilePath
        };
    }

    private async Task<string> UploadFileAsync(IFormFile file, int userId)
    {
        try
        {
            // Defining the base directory for file uploads within the project folder
            var baseDirectory = Path.Combine(_environment.ContentRootPath, "uploads");

            // Define the path to save the file in a user-specific directory
            var userFolder = Path.Combine(baseDirectory, userId.ToString());
            Directory.CreateDirectory(userFolder); // Ensure the user directory exists

            // Generate a unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(userFolder, uniqueFileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Generate a public URL to access the file
            var request = _httpContextAccessor.HttpContext.Request;
            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{userId}/{uniqueFileName}";

            return fileUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while uploading the file.");
            throw;
        }
    }
}