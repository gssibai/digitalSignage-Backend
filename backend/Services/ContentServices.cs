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
        if (contentDto.File == null)
        {
            _logger.LogError("File is null or missing.");
            throw new ArgumentException("File is required.");
        }

        if (contentDto.File.Length == 0)
        {
            _logger.LogError("File length is zero.");
            throw new ArgumentException("File cannot be empty.");
        }

        var content = new Content
        {
            UserId = contentDto.UserId,
            Title = contentDto.Title,
            Type = contentDto.Type,
            Description = contentDto.Description,
            StartTime = contentDto.StartTime,
            EndTime = contentDto.EndTime
        };

        try
        {
            // Defining the base directory for file uploads within the project folder
            var baseDirectory = Path.Combine(_environment.ContentRootPath, "uploads");

            // Define the path to save the file in a user-specific directory
            var userFolder = Path.Combine(baseDirectory, content.UserId.ToString());
            Directory.CreateDirectory(userFolder); // Ensure the user directory exists

            // Generate a unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(contentDto.File.FileName);
            var filePath = Path.Combine(userFolder, uniqueFileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await contentDto.File.CopyToAsync(fileStream);
            }

            // Generate a public URL to access the file
            var request = _httpContextAccessor.HttpContext.Request;
            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{contentDto.UserId}/{uniqueFileName}";


            // Set the file path and other content properties
            content.FilePath = fileUrl;
            content.CreatedAt = DateTime.UtcNow;
            content.UpdatedAt = DateTime.UtcNow;

            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();

            return new ContentResponseDto
            {
                ContentId = content.ContentId,
                UserId = content.UserId,
                Title = content.Title,
                Type = content.Type,
                Description = content.Description,
                //File = content.File,
                StartTime = content.StartTime,
                EndTime = content.EndTime,
                CreatedAt = content.CreatedAt
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

//     public async Task<Content> UpdateContentAsync(int contentId, Content content)
//     {
//        
//         

}