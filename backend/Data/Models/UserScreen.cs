using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalSignageApi.Models;

namespace backend.Data.Models;

public class UserScreen
{
    [Key]
    public int UserScreenId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Screen))]
    public int ScreenId { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Screen Screen { get; set; }
}