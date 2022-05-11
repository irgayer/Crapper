using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF
{
    public class AppContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            
        }
    }
}
