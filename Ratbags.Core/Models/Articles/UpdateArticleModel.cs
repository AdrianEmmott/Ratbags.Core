using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class UpdateArticleModel
{
    [Required(ErrorMessage = "Article id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime? Published { get; set; }

    public List<CommentDTO>? Comments { get; set; } = new List<CommentDTO>() { };
}
