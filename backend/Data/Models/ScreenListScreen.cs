using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalSignageApi.Models
{
    public class ScreenListScreen
    {
      [Key]
        public int ListScreenId { get; set; }
       [ForeignKey(nameof(ScreenList))]
        public int ListId { get; set; }
        public ScreenList ScreenList { get; set; } // Navigation to ScreenList

     [ForeignKey(nameof(Screen))]
        public int ScreenId { get; set; }
        public Screen Screen { get; set; } // Navigation to Screen
    }

}