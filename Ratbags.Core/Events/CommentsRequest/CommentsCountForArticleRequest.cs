namespace Ratbags.Core.Events.CommentsRequest;

public class CommentsCountForArticleRequest
{
    public Guid ArticleId { get; set; }
    public DateTime Timestamp { get; set; }
}
