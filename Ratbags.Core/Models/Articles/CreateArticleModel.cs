using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.Models.Articles;

public class CreateArticleModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = null!;

    public DateTime Created { get; set; }
}
