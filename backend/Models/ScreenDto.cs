using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class ScreenDTO
{
    public int ScreenId { get; set; }
    public string ScreenName { get; set; }
    public string Location { get; set; }
    public string ConnectionCode { get; set; }
}
public class CreateScreenDTO
{
    [MaxLength(50)]
    public required string ScreenName { get; set; }

    [MaxLength(100)]
    public required string Location { get; set; }
    

}
public class UpdateScreenDTO
{
  //  public int ScreenId { get; set; }

    [MaxLength(50)]
    public string? ScreenName { get; set; }

    [MaxLength(100)]
    public string? Location { get; set; }
}
public class ConnectUserRequest
{
    public int UserId { get; set; }
    public string Code { get; set; }
}