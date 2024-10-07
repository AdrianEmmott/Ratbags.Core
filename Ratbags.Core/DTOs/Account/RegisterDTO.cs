using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Account;

public class RegisterDTO
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}
