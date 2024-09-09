using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DigitalSignageApi.Models
{
    public class ScreenTag
    {
       [Key]
        public int TagId { get; set; }
      [ForeignKey(nameof(Screen))]
        public int ScreenId { get; set; }
        [MaxLength(50)]
        public required string TagName { get; set; }
        
        // Navigation property
        public required Screen Screen { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual User User { get; set; }
    }
}