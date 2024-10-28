using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalSignageApi.Models;

namespace backend.Data.Models;

public class AssetAssignment
{
  [Key]
    public int AssignmentId { get; set; }
    [ForeignKey(nameof(Screen))]
    public int ScreenId { get; set; }
    [ForeignKey(nameof(Content))]
    public int ContentId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpiresAt { get; set; }
    
}