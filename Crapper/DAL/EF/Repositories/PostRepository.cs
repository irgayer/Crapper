using System.Linq.Expressions;
using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly EFContext _context;

        public PostRepository(EFContext context)
        {
            _context = context;
        }

        public void Add(Post entity)
        {
            _context.Posts.Add(entity);
        }

        public void Update(Post entity)
        {
            _context.Posts.Update(entity);
        }

        public void Delete(Post entity)
        {
            _context.Posts.Remove(entity);
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts.AsNoTracking().AsQueryable();
        }

        public IQueryable<Post> Find(Expression<Func<Post, bool>> predicate)
        {
            return _context.Posts.AsNoTracking().Where(predicate).AsQueryable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
