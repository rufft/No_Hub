using System.ComponentModel.DataAnnotations;

namespace No_Hub.Domain.Data.DTOs;

public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 4)]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password and confirm password didn't match")]
    public string ConfirmPassword { get; set; }
}