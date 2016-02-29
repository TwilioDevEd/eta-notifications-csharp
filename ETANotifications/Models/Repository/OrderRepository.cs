using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ETANotifications.Models.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Task<Order> FindAsync(int? id);
        Task<IEnumerable<Order>> FindAllAsync();
        Task UpdateAsync(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context = new OrderContext();

        public async Task<Order> FindAsync(int? id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> FindAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}