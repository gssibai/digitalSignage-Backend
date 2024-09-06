using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace DigitalSignageApi.Models
{
    enum ContentType
    {
        Image,
        Video,
        Text
    }

    public class Content
    {
        [Key]
        public int ContentId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Change to string to match IdentityUser's 'Id' type

        [EnumDataType(typeof(ContentType))]
        public required string Type { get; set; }

        public string FilePath { get; set; }

        [MaxLength(100)]
        public required string Title { get; set; }

        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual User User { get; set; }

        // [NotMapped]
        // public IFormFile File { get; set; }
    }
}