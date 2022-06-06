using System.Linq.Expressions;
using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Crapper.DAL.EF.Repositories
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly EFContext _context;

        public SubscriptionRepository(EFContext context)
        {
            _context = context;
        }
        public async Task Add(Subscription entity)
        {
            await _context.Subscriptions.AddAsync(entity);
        }

        public async Task Update(Subscription entity)
        {
            await _context.Subscriptions.Update(entity).ReloadAsync();
        }

        public void Delete(Subscription entity)
        {
            _context.Subscriptions.Remove(entity);
        }

        public IQueryable<Subscription> GetAll()
        {
            return _context.Subscriptions;
        }

        public IQueryable<Subscription> Find(Expression<Func<Subscription, bool>> predicate)
        {
            return _context.Subscriptions.Where(predicate);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Subscription?> GetById(int id)
        {
            return await _context.Subscriptions.SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}
