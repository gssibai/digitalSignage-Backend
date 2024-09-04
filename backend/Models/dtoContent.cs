using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalSignageApi.Models;

namespace backend.Models
{

    public class dtoContent
    {
        public int ContentId { get; set; }

        [Required] public int UserId { get; set; }

        [Required]
        [EnumDataType(typeof(ContentType))]
        public string Type { get; set; }

        [MaxLength(100)] [Required] public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

       // public string FilePath { get; set; }
        public IFormFile File { get; set; }
    }

    public class ContentResponseDto
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; } // Only show the file path if needed
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}