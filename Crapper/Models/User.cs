using System.ComponentModel.DataAnnotations;

namespace Crapper.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        public string Email { get; set; }
        
        [StringLength(300)]
        public string? Bio { get; set; }

        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
        
        public ICollection<Subscription> Subscribers { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public int SubscribersCount 
        { 
            get
            {
                return Subscribers.Count;
            } 
        }

        public int SubscriptionsCount
        {
            get
            {
                return Subscriptions.Count;
            }
        }
    }
}
