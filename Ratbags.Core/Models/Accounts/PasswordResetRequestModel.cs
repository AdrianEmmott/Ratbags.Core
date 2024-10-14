using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.Models.Accounts;

public class PasswordResetRequestModel
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;
}
