﻿using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles.Comments;

public class CreateCommentDTO
{
    [Required(ErrorMessage = "Article id is required")]
    public Guid ArticleId { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    public string Content { get; set; } = null!;

    [Required(ErrorMessage = "User id is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Publish date id is required")]
    public DateTime Published { get; set; }
}