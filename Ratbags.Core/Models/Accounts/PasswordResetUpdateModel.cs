using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.Models.Accounts;

public class PasswordResetUpdateModel
{
    [Required(ErrorMessage = "User id is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "PasswordResetToken id is required")]
    public string PasswordResetToken { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}
