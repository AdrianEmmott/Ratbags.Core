using Ratbags.Core.DTOs.Articles.Comments;

namespace Ratbags.Core.Events.CommentsRequest;

public class CommentsForArticleResponse
{
    public Guid ArticleId { get; set; }
    public List<CommentDTO> Comments { get; set; }
}
