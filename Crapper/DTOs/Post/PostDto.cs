namespace Crapper.DTOs.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        //todo: search better solution
        public string AuthorUsername { get; set; }
        public int Likes { get; set; }
    }
}
