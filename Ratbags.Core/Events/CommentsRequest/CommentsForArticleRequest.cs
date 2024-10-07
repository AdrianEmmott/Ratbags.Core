namespace Ratbags.Core.Events.CommentsRequest;

public class CommentsForArticleRequest
{
    public Guid ArticleId { get; set; }
    public DateTime Timestamp { get; set; }
}
