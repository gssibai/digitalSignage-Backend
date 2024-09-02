using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignageApi.Models
{
    public class Screen
    {
       [Key]
        public int ScreenId { get; set; }
       [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [MaxLength(50)]
        public required string ScreenName { get; set; }
        [MaxLength(100)]
        public required string Location { get; set; }
        // public string Status { get; set; }
        
        
        public User User { get; set; }
        public ICollection<ScreenTag> Tags { get; set; } // Navigation to ScreenTags
        public ICollection<ScreenListScreen> ScreenListScreens { get; set; } // Navigation to ScreenListScreens
        //public ICollection<Schedule> Schedules { get; set; } // Navigation to Schedules
    }

}