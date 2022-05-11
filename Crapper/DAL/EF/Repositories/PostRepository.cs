using System.Linq.Expressions;
using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly AppContext _context;

        public PostRepository(AppContext context)
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
    }
}
