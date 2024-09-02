using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignageApi.Models
{
    public class ScreenList
    {
        [Key]
        public int ListId { get; set; }
       [ForeignKey(nameof(User))]
        public int UserId { get; set; }
       [MaxLength(50)]
        public required string ListName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? User { get; set; }
        public ICollection<ScreenListScreen> ScreenListScreens { get; set; }
    }
}