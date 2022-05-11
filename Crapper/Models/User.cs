namespace Crapper.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
    }
}
