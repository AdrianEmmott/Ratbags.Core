using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.Models.Articles;

public class CreateArticleModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } // tag line for lists

    public string? Introduction { get; set; } // introduction on article itself

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = string.Empty;

    public string? BannerImageUrl { get; set; }

    public DateTime Created { get; set; }
}