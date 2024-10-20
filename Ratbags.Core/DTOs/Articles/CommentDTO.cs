using System.ComponentModel.DataAnnotations;

namespace Ratbags.Core.DTOs.Articles;

public class CommentDTO
{
    public Guid Id { get; set; }

    public string CommenterName {  get; set; }=string.Empty;

    public string Content { get; set; } = null!;

    public DateTime Published { get; set; }
}
