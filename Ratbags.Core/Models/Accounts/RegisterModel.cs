using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.Models.Accounts;

public class RegisterModel
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}
