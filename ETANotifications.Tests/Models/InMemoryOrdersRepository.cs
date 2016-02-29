using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETANotifications.Models;
using ETANotifications.Models.Repository;

namespace ETANotifications.Tests.Models
{
    class InMemoryOrdersRepository: IOrderRepository
    {
        List<Order> orders = new List<Order>
                                {
                                    new Order {Id = 1,
                                                CustomerName = "Jack",
                                                CustomerPhoneNumber = "+4555555555",
                                                Status = "Ready",
                                                NotificationStatus = "None"},
                                    new Order {Id = 2,
                                                CustomerName = "Caesar",
                                                CustomerPhoneNumber = "+678899000",
                                                Status = "Ready",
                                                NotificationStatus = "None"}
                                };

        public void Dispose()
        {
            
        }

        public Task<Order> FindAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Task.FromResult(orders.First(ord => ord.Id == id.Value));
        }

        public Task<IEnumerable<Order>> FindAllAsync()
        {
            return Task.FromResult((IEnumerable<Order>)orders);
        }

        public Task UpdateAsync(Order order)
        {
            return Task.FromResult(0);
        }
    }
}
