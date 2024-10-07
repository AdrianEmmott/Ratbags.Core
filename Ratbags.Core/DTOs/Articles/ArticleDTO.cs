﻿using System.ComponentModel.DataAnnotations;
using Ratbags.Core.DTOs.Articles.Comments;

namespace Ratbags.Core.DTOs.Articles;

public class ArticleDTO
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