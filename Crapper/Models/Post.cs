namespace Crapper.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public int UserId { get; set; }
    }
}
