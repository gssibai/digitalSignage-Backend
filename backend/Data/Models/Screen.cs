using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Models;

namespace DigitalSignageApi.Models
{
    public class Screen
    {
        [Key]
        public int ScreenId { get; set; }

        [MaxLength(50)]
        public required string ScreenName { get; set; }

        [MaxLength(100)]
        public required string Location { get; set; }

        // Code and Barcode for user connection
        [MaxLength(10)]
        public string ConnectionCode { get; set; }
        
        
        // Navigation properties
        public ICollection<ScreenTag> Tags { get; set; } // Navigation to ScreenTags
        public ICollection<ScreenListScreen> ScreenListScreens { get; set; } // Navigation to ScreenListScreens
    
        // Many-to-Many relationship with User
        public ICollection<UserScreen> UserScreens { get; set; }
    }

}