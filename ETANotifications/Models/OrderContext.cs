using System.Data.Entity;

namespace ETANotifications.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}