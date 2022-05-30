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

        public async Task Add(Post entity)
        {
            await _context.Posts.AddAsync(entity);
        }

        public async Task Update(Post entity)
        {
            await _context.Posts.Update(entity).ReloadAsync();
        }

        public void Delete(Post entity)
        {
            _context.Posts.Remove(entity);
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts.Include(post => post.Author).AsNoTracking();
        }

        public IQueryable<Post> Find(Expression<Func<Post, bool>> predicate)
        {
            return _context.Posts.Include(post => post.Author).Where(predicate).AsNoTracking();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Post?> GetById(int id)
        {
            return await _context.Posts.SingleOrDefaultAsync(post => post.Id == id);
        }
    }
}
