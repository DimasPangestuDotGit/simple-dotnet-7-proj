using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be 5 Characters!")]
    [MaxLength(250, ErrorMessage = "Title cannot more than 250 characters!")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage = "Content must be 5 Characters!")]
    [MaxLength(250, ErrorMessage = "Content cannot more than 250 characters!")]
    public string Content { get; set; } = string.Empty;
}