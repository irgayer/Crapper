using System.ComponentModel.DataAnnotations;

namespace Crapper.DTOs.Post
{
    public class PostCreateDto
    {
        [StringLength(300, ErrorMessage = "Content must be 1 to 300 characters long", MinimumLength = 1)]
        public string Content { get; set; }
    }
}
