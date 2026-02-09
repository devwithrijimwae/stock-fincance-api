using System.ComponentModel.DataAnnotations;

namespace stock_fincance_api.DTOs.Comments
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 character")]
        [MaxLength(280, ErrorMessage = "Title must be 280 character")]

        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 character")]
        [MaxLength(280, ErrorMessage = "Content must be 280 character")]
        public string Content { get; set; } = string.Empty;
    }
}
