// using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace DigitalSignageApi.Models
// {
//     public class Schedule
//     {
//       [Key]
//         public int ScheduleId { get; set; }
//        [ForeignKey(nameof(Content))]
//         public int ContentId { get; set; }
//        [ForeignKey(nameof(Screen))]
//         public int ScreenId { get; set; }
//         public DateTime StartTime { get; set; }
//         public DateTime EndTime { get; set; }
//         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//         public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
//
//
//         // Navigation properties
//         public Content Content { get; set; }
//         public Screen Screen { get; set; }
//     }
// }