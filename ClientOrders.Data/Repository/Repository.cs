using ClientOrders.Data.Database;
using ClientOrders.Data.Models.Common;
using ClientOrders.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientOrders.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public T Add(T item)
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(T);

            T result = dbSet.Add(item).Entity;
            _context.SaveChanges();

            return result;
        }

        public List<T> AddAll(IEnumerable<T> items)
        {
            List<T> result = new List<T>();

            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(List<T>);

            foreach (T item in items)
            {
                T entity = dbSet.Add(item).Entity;
                result.Add(entity);
            }

            _context.SaveChanges();
            return result;
        }

        public void Delete(T item)
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return;

            dbSet.Remove(item);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(List<T>);

            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(T);

            var item = dbSet.FirstOrDefault(z => z.Id == id);
            return item;
        }
        public async Task<IEnumerable<Order>> GetByClientIdAsync(int clientId)
        {
            return await _context.Orders.Where(o => o.ClientID == clientId).ToListAsync();
        }

        public void Update(T item)
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return;

            dbSet.Update(item);

            _context.SaveChanges();
        }
        public async Task<List<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _context.Orders
                .Where(o => o.ClientID == clientId)
                .ToListAsync();
        }
    }
}