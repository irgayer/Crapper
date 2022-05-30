using System.Linq.Expressions;
using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly EFContext _context;

        public UserRepository(EFContext context)
        {
            _context = context;
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public async Task Update(User entity)
        {
            await _context.Users.Update(entity).ReloadAsync();
        }

        public async Task Delete(User entity)
        {
            await _context.Users.Remove(entity).ReloadAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }

        public IQueryable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate)
                .Include(user => user.Subscribers)
                .Include(user => user.Subscriptions)
                .Include(user => user.Posts)
                .AsNoTracking();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .Include(user => user.Subscribers)
                .Include(user => user.Subscriptions)
                .Include(user => user.Posts)
                .SingleOrDefaultAsync(user => user.Id == id);
        }
    }
}
