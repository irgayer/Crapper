using System.ComponentModel.DataAnnotations;

namespace Crapper.Models
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(300, MinimumLength = 1)]
        public string Content { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
    }
}
