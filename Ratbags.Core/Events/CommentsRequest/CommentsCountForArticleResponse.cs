namespace Ratbags.Core.Events.CommentsRequest;

public class CommentsCountForArticleResponse
{
    public Guid ArticleId { get; set; }
    public int Count { get; set; }
}
