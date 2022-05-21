namespace Crapper.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        //just count
        public int Subscribers { get; set; } 
        public int Subscriptions { get; set; }
        public int Posts { get; set; }
    }
}
