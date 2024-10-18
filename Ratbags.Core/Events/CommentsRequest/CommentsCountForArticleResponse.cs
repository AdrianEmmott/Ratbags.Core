namespace Ratbags.Core.Events.CommentsRequest;

public class SendRegisterConfirmEmailResponse
{
    public Guid ArticleId { get; set; }
    public int Count { get; set; }
}
