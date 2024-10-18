using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class ArticleListDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Image { get; set;} = null!;
    public string Description { get; set; } = null!;
    public DateTime? Published { get; set; }
    public int CommentCount { get; set; } // TODO should probably have a separate dto for article lists...
}
