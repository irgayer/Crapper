using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF
{
    public class EFContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            
        }
    }
}
