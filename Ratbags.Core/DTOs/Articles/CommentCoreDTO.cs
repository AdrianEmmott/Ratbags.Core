using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public sealed record CommentCoreDTO(
    Guid Id,
    Guid UserId,
    string Content,
    DateTimeOffset Published
);
