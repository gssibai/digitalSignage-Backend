using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class UserDto
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class NewUserDto
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    public string Password { get; set; }
    
}

public class LoginUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UpdateUserDto
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Password { get; set; }
}