using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class CreateArticleDTO
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = null!;

    public DateTime Created { get; set; }
}
