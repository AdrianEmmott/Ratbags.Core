using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class UpdateCommentModel
{
    [Required(ErrorMessage = "Comment id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Article id is required")]
    public Guid ArticleId { get; set; }

    [Required(ErrorMessage = "User id is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    public string Content { get; set; } = null!;

    [Required(ErrorMessage = "Publish date is required")]
    public DateTime Published { get; set; }
}
