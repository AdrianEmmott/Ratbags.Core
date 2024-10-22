using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class UpdateArticleModel
{
    [Required(ErrorMessage = "Article id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } // tag line for lists

    public string? Introduction { get; set; } // introduction on article itself

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = string.Empty;

    public string? BannerImageUrl { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime? Published { get; set; }
}
