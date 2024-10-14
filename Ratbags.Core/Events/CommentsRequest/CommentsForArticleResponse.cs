using Ratbags.Core.DTOs.Articles;

namespace Ratbags.Core.Events.CommentsRequest;

public class CommentsForArticleResponse
{
    public Guid ArticleId { get; set; }
    public List<CommentDTO> Comments { get; set; }
}
