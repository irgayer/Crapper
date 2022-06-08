using Crapper.Interfaces;
using Crapper.Models;

using System.Linq.Expressions;

namespace Crapper.DAL.EF.Repositories
{
    public class LikeRepository : IRepository<Like>
    {
        private readonly EFContext _context;

        public LikeRepository(EFContext context)
        {
            _context = context;
        }

        public async Task Add(Like entity)
        {
            await _context.Likes.AddAsync(entity);
        }

        public void Delete(Like entity)
        {
            _context.Likes.Remove(entity);
        }

        public IQueryable<Like> Find(Expression<Func<Like, bool>> predicate)
        {
            return _context.Likes.Where(predicate);
        }

        public IQueryable<Like> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Like?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(Like entity)
        {
            throw new NotImplementedException();
        }
    }
}
