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
        public void Add(Subscription entity)
        {
            _context.Subscriptions.Add(entity);
        }

        public void Update(Subscription entity)
        {
            _context.Subscriptions.Update(entity);
        }

        public void Delete(Subscription entity)
        {
            _context.Subscriptions.Remove(entity);
        }

        public IQueryable<Subscription> GetAll()
        {
            return _context.Subscriptions.AsNoTracking().AsQueryable();
        }

        public IQueryable<Subscription> Find(Expression<Func<Subscription, bool>> predicate)
        {
            return _context.Subscriptions.Where(predicate).AsNoTracking().AsQueryable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
