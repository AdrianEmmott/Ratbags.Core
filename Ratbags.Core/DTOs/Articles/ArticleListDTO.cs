using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class ArticleListDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ThumbnailImageUrl { get; set;}
    public string? Description { get; set; } // targ line
    public DateTime Created { get; set; }
    public DateTime? Published { get; set; }
    public int CommentCount { get; set; }
}
