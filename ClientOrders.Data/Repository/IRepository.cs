using ClientOrders.Data.Models.Common;
using ClientOrders.Data.Models.Entities;

namespace ClientOrders.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public T Add(T item);
        public List<T> AddAll(IEnumerable<T> items);
        public List<T> GetAll();
        public T GetById(int id);
        public void Update(T item);
        public void Delete(T item);
        public Task<IEnumerable<Order>> GetByClientIdAsync(int clientId);
        public Task<List<Order>> GetOrdersByClientIdAsync(int clientId);

    }
}
