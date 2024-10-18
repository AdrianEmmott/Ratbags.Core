namespace Ratbags.Core.Events.Accounts;

public class SendForgotPasswordEmailRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}
