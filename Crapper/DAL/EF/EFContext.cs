using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF
{
    public class EFContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.To)
                .WithMany(user => user.Subscribers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.From)
                .WithMany(user => user.Subscriptions)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
