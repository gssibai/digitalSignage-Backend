using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace DigitalSignageApi.Models
{
    enum Role
    {
        User,
        Admin
    };
    public class User 
    {
        [Key]
        public int UserId { get; set; }
        public string? GoogleId { get; set; }
        [MaxLength(50)]
        public required string Email { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(50)]
        public required string Surname { get; set; }
        [MaxLength(50)]
        public required string PasswordHash { get; set; }
       [EnumDataType(typeof(Role))]
        public required string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Content> Contents { get; set; }
        public ICollection<Screen> Screens { get; set; }
        public ICollection<ScreenList> ScreenLists { get; set; }
    }
}