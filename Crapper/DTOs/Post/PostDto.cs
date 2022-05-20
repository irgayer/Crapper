namespace Crapper.DTOs.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; }
    }
}
