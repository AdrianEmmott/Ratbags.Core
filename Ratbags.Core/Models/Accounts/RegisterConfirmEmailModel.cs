using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.Models.Accounts;

public class RegisterConfirmEmailModel
{
    [Required(ErrorMessage = "User id is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Token is required")]
    public string Token { get; set; }
}
